using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HorCup.Infrastructure.Services.IdGenerator;
using HorCup.Plays.Context;
using HorCup.Plays.Models;
using HorCup.Plays.Shared.Dto;
using HorCup.Plays.Shared.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HorCup.Plays.Commands.AddPlay
{
	public class AddPlayCommandHandler : IRequestHandler<AddPlayCommand, Guid>
	{
		private readonly IIdGenerator _idGenerator;
		private readonly IPlaysContext _context;
		private readonly ILogger<AddPlayCommandHandler> _logger;
		private readonly IMapper _mapper;
		private readonly IPublishEndpoint _endpoint;

		public AddPlayCommandHandler(
			IIdGenerator idGenerator,
			IPlaysContext context,
			ILogger<AddPlayCommandHandler> logger,
			IMapper mapper,
			IPublishEndpoint endpoint)
		{
			_idGenerator = idGenerator;
			_context = context;
			_logger = logger;
			_mapper = mapper;
			_endpoint = endpoint;
		}

		public async Task<Guid> Handle(AddPlayCommand request, CancellationToken cancellationToken)
		{
			var playId = _idGenerator.NewGuid();

			request.Id = playId;

			_logger.LogInformation($"Adding Play with id {playId}");
			
			SetWinner(request);

			await _context.Plays.InsertOneAsync(_mapper.Map<Play>(request), cancellationToken: cancellationToken);

			await _endpoint.Publish(new GamePlayed
			{
				GameId = request.Game.Id,
				AverageScore = request.PlayerScores.Average(p => p.Score),
				LastPlayedDate = request.PlayedDate,
				PlayedScores = _mapper.Map<IEnumerable<PlayerScoresDto>>(request.PlayerScores)
			}, cancellationToken);

			return playId;
		}

		private static void SetWinner(AddPlayCommand request)
		{
			var highestScore = request.PlayerScores.Select(s => s.Score).Max();

			foreach (var playScore in request.PlayerScores)
			{
				playScore.IsWinner = playScore.Score == highestScore;
			}
		}
	}
}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HorCup.Infrastructure.Services.IdGenerator;
using HorCup.Plays.Context;
using HorCup.Presentation.PlayScores;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Plays.Commands.AddPlay
{
	public class AddPlayCommandHandler : IRequestHandler<AddPlayCommand, Guid>
	{
		private readonly IIdGenerator _idGenerator;
		private readonly IPlaysContext _context;
		private readonly ILogger<AddPlayCommandHandler> _logger;
		private readonly IPublisher _publisher;

		public AddPlayCommandHandler(
			IIdGenerator idGenerator,
			IPlaysContext context,
			ILogger<AddPlayCommandHandler> logger,
			IPublisher publisher)
		{                              
			_idGenerator = idGenerator;
			_context = context;
			_logger = logger;
			_publisher = publisher;
		}

		public async Task<Guid> Handle(AddPlayCommand request, CancellationToken cancellationToken)
		{
			await ValidateGame(request.PlayerScores.Count());

			var playId = _idGenerator.NewGuid();

			await _context.Plays.AddAsync(new Play
			{
				Id = playId,
				GameId = request.GameId,
				Notes = request.Notes,
				PlayedDate = request.PlayedDate,
			}, cancellationToken);

			var highestScore = request.PlayerScores.Select(s => s.Score).Max();

			var playScores = request.PlayerScores.Select(s => new PlayScore
			{
				Score = s.Score,
				IsWinner = highestScore == s.Score,
				PlayId = playId,
				PlayerId = s.Player.Id
			}).ToArray();
			
			await _context.PlayScores.AddRangeAsync(playScores, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);

			// await _publisher.Publish(new GamePlayedNotification(
			// 	request.GameId,
			// 	request.PlayerScores.Average(p => p.Score),
			// 	request.PlayedDate,
			// 	playScores), cancellationToken);

			return playId;

			async Task ValidateGame(int playerScoresCount)
			{
				// var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);
				//
				// if (game == null)
				// {
				// 	_logger.LogError($"Game with id {request.GameId} was not found");
				// 	throw new NotFoundException(nameof(Game), request.GameId);
				// }
				//
				// if (playerScoresCount > game.MaxPlayers)
				// {
				// 	_logger.LogError("Players count is bigger than game max players");
				// 	throw new ArgumentException("Players count cannot be bigger than game maximum players");
				// }
				//
				// if (game.MinPlayers > playerScoresCount)
				// {
				// 	_logger.LogError("Players count is less than game min players");
				// 	throw new ArgumentException("Players count cannot be less than game minimum players");
				// }
			}
		}
	}
}
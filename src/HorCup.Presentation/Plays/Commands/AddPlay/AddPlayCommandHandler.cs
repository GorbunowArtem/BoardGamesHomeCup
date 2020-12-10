using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.Games;
using HorCup.Presentation.PlayScores;
using HorCup.Presentation.Services.DateTimeService;
using HorCup.Presentation.Services.IdGenerator;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Presentation.Plays.Commands.AddPlay
{
	public class AddPlayCommandHandler : IRequestHandler<AddPlayCommand, Guid>
	{
		private readonly IIdGenerator _idGenerator;
		private readonly IDateTimeService _dateTimeService;
		private readonly IHorCupContext _context;

		public AddPlayCommandHandler(
			IIdGenerator idGenerator,
			IDateTimeService dateTimeService,
			IHorCupContext context)
		{
			_idGenerator = idGenerator;
			_dateTimeService = dateTimeService;
			_context = context;
		}

		public async Task<Guid> Handle(AddPlayCommand request, CancellationToken cancellationToken)
		{
			await CheckGameExists();

			var playId = _idGenerator.NewGuid();

			await _context.Plays.AddAsync(new Play
			{
				Id = playId,
				GameId = request.GameId,
				Notes = request.Notes,
				PlayedDate = _dateTimeService.Now,
			}, cancellationToken);

			await _context.PlayScores.AddRangeAsync(
				request.PlayerScores.Select(s => new PlayScore
				{
					Score = s.Score,
					IsWinner = s.IsWinner,
					PlayId = playId,
					PlayerId = s.Player.Id
				}), cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);

			return playId;
			
			async Task CheckGameExists()
			{
				var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);

				if (game == null)
				{
					throw new NotFoundException(nameof(Game), request.GameId);
				}
			}
		}
	}
}
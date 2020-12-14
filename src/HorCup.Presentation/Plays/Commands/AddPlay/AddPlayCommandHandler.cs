using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.Games;
using HorCup.Presentation.PlayScores;
using HorCup.Presentation.Services.IdGenerator;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Plays.Commands.AddPlay
{
	public class AddPlayCommandHandler : IRequestHandler<AddPlayCommand, Unit>
	{
		private readonly IIdGenerator _idGenerator;
		private readonly IHorCupContext _context;
		private readonly ILogger<AddPlayCommandHandler> _logger;

		public AddPlayCommandHandler(
			IIdGenerator idGenerator,
			IHorCupContext context,
			ILogger<AddPlayCommandHandler> logger)
		{
			_idGenerator = idGenerator;
			_context = context;
			_logger = logger;
		}

		public async Task<Unit> Handle(AddPlayCommand request, CancellationToken cancellationToken)
		{
			await CheckGameExists();

			await _context.Plays.AddAsync(new Play
			{
				Id = request.Id,
				GameId = request.GameId,
				Notes = request.Notes,
				PlayedDate = request.PlayedDate,
			}, cancellationToken);

			await _context.PlayScores.AddRangeAsync(
				request.PlayerScores.Select(s => new PlayScore
				{
					Score = s.Score,
					IsWinner = s.IsWinner,
					PlayId = request.Id,
					PlayerId = s.Player.Id
				}), cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
			
			async Task CheckGameExists()
			{
				var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);

				if (game == null)
				{
					_logger.LogError($"Game with id {request.GameId} was not found");
					throw new NotFoundException(nameof(Game), request.GameId);
				}
			}
		}
	}
}
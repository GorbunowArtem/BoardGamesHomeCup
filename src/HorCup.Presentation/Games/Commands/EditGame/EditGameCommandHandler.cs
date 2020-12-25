using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.Services.Games;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Games.Commands.EditGame
{
	public class EditGameCommandHandler : IRequestHandler<EditGameCommand, Unit>
	{
		private readonly IHorCupContext _context;
		private readonly IGamesService _gamesService;
		private readonly ILogger<EditGameCommandHandler> _logger;

		public EditGameCommandHandler(
			IHorCupContext context,
			IGamesService gamesService,
			ILogger<EditGameCommandHandler> logger)
		{
			_context = context;
			_gamesService = gamesService;
			_logger = logger;
		}

		public async Task<Unit> Handle(EditGameCommand request, CancellationToken cancellationToken)
		{
			var (id, title, maxPlayers, minPlayers, _) = request;
			
			var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

			if (game == null)
			{
				throw new NotFoundException(nameof(Game), id);
			}

			var isTitleUnique = await _gamesService.IsTitleUniqueAsync(title, id);

			if (!isTitleUnique)
			{
				throw new EntityExistsException(nameof(Game), id);
			}

			game.Title = title;
			game.MaxPlayers = maxPlayers;
			game.MinPlayers = minPlayers;

			_context.Games.Update(game);

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
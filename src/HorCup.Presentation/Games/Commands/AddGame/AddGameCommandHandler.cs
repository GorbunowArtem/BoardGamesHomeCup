using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.Services.DateTimeService;
using HorCup.Presentation.Services.Games;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Games.Commands.AddGame
{
	public class AddGameCommandHandler: IRequestHandler<AddGameCommand, Unit>
	{
		private readonly IHorCupContext _context;
		private readonly ILogger<AddGameCommandHandler> _logger;
		private readonly IGamesService _gamesService;
		private readonly IDateTimeService _dateTimeService;

		public AddGameCommandHandler(IHorCupContext context,
			ILogger<AddGameCommandHandler> logger,
			IGamesService gamesService,
			IDateTimeService dateTimeService)
		{
			_context = context;
			_logger = logger;
			_gamesService = gamesService;
			_dateTimeService = dateTimeService;
		}

		public async Task<Unit> Handle(AddGameCommand request, CancellationToken cancellationToken)
		{
			var isTitleUnique = await _gamesService.IsTitleUniqueAsync(request.Title);
			
			if (!isTitleUnique)
			{
				_logger.LogError($"Game with title {request.Title} already exists");
				throw new EntityExistsException(nameof(Game), request.Title);
			}

			var game = new Game
			{
				Id = request.Id,
				Title = request.Title,
				MaxPlayers = request.MaxPlayers,
				MinPlayers = request.MinPlayers,
				Added = _dateTimeService.Now,
				HasScores = request.HasScores
			};

			_logger.LogInformation($"Adding game with id {request.Id} title {request.Title}");
			
			await _context.Games.AddAsync(game, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
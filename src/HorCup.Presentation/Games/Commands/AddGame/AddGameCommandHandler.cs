using System;
using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.Services.DateTimeService;
using HorCup.Presentation.Services.Games;
using HorCup.Presentation.Services.IdGenerator;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Games.Commands.AddGame
{
	public class AddGameCommandHandler: IRequestHandler<AddGameCommand, Guid>
	{
		private readonly IHorCupContext _context;
		private readonly ILogger<AddGameCommandHandler> _logger;
		private readonly IGamesService _gamesService;
		private readonly IDateTimeService _dateTimeService;
		private readonly IIdGenerator _idGenerator;

		public AddGameCommandHandler(IHorCupContext context,
			ILogger<AddGameCommandHandler> logger,
			IGamesService gamesService,
			IDateTimeService dateTimeService,
			IIdGenerator idGenerator)
		{
			_context = context;
			_logger = logger;
			_gamesService = gamesService;
			_dateTimeService = dateTimeService;
			_idGenerator = idGenerator;
		}

		public async Task<Guid> Handle(AddGameCommand request, CancellationToken cancellationToken)
		{
			var isTitleUnique = await _gamesService.IsTitleUniqueAsync(request.Title, null);
			
			if (!isTitleUnique)
			{
				_logger.LogError($"Game with title {request.Title} already exists");
				throw new EntityExistsException(nameof(Game), request.Title);
			}

			var id = _idGenerator.NewGuid();
			
			var game = new Game
			{
				Id = id,
				Title = request.Title,
				MaxPlayers = request.MaxPlayers,
				MinPlayers = request.MinPlayers,
				Added = _dateTimeService.Now,
				HasScores = request.HasScores
			};

			_logger.LogInformation($"Adding game with id {id.ToString()} title {request.Title}");
			
			await _context.Games.AddAsync(game, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);

			return id;
		}
	}
}
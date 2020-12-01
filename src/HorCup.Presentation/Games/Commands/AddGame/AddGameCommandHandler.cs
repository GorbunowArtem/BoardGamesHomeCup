using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HorCup.Presentation.Context;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.Services.Games;
using HorCup.Presentation.Services.IdGenerator;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Games.Commands.AddGame
{
	public class AddGameCommandHandler: IRequestHandler<AddGameCommand, GameViewModel>
	{
		private readonly IHorCupContext _context;
		private readonly IIdGenerator _idGenerator;
		private readonly ILogger<AddGameCommandHandler> _logger;
		private readonly IMapper _mapper;
		private readonly IGamesService _gamesService;

		public AddGameCommandHandler(IHorCupContext context,
			IIdGenerator idGenerator,
			IMapper mapper,
			ILogger<AddGameCommandHandler> logger,
			IGamesService gamesService)
		{
			_context = context;
			_idGenerator = idGenerator;
			_mapper = mapper;
			_logger = logger;
			_gamesService = gamesService;
		}

		public async Task<GameViewModel> Handle(AddGameCommand request, CancellationToken cancellationToken)
		{
			var isTitleUnique = await _gamesService.IsTitleUniqueAsync(request.Title);
			
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
				MinPlayers = request.MinPlayers
			};

			_logger.LogInformation($"Adding game with id {id} title {request.Title}");
			
			await _context.Games.AddAsync(game, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<GameViewModel>(game);
		}
	}
}
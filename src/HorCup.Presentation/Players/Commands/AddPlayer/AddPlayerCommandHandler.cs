using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HorCup.Presentation.Context;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.Services.IdGenerator;
using HorCup.Presentation.Services.Players;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Players.Commands.AddPlayer
{
	public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, PlayerViewModel>
	{
		private readonly HorCupContext _context;
		private readonly IIdGenerator _idGenerator;
		private readonly IMapper _mapper;
		private readonly ILogger<AddPlayerCommandHandler> _logger;
		private readonly IPlayersService _playersService;
		
		public AddPlayerCommandHandler(
			HorCupContext context,
			IIdGenerator idGenerator,
			IMapper mapper,
			ILogger<AddPlayerCommandHandler> logger,
			IPlayersService playersService)
		{
			_context = context;
			_idGenerator = idGenerator;
			_mapper = mapper;
			_logger = logger;
			_playersService = playersService;
		}

		public async Task<PlayerViewModel> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
		{
			var isUnique = await _playersService.IsNicknameUniqueAsync(request.Nickname);

			if (!isUnique)
			{
				_logger.LogError($"Player with nickname {request.Nickname} already exists");
				throw new EntityExistsException(nameof(Player), request.Nickname);
			}
			
			var id = _idGenerator.NewGuid();

			var player = new Player
			{
				Id = id,
				FirstName = request.FirstName,
				LastName = request.LastName,
				BirthDate = request.BirthDate,
				Nickname = request.Nickname
			};

			await _context.AddAsync(player, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<PlayerViewModel>(player);
		}
	}
}
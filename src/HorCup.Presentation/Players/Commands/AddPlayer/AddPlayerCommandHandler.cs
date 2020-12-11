using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.Services.DateTimeService;
using HorCup.Presentation.Services.Players;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Players.Commands.AddPlayer
{
	public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, Unit>
	{
		private readonly IHorCupContext _context;
		private readonly ILogger<AddPlayerCommandHandler> _logger;
		private readonly IPlayersService _playersService;
		private readonly IDateTimeService _dateTimeService;
		
		public AddPlayerCommandHandler(
			IHorCupContext context,
			ILogger<AddPlayerCommandHandler> logger,
			IPlayersService playersService,
			IDateTimeService dateTimeService)
		{
			_context = context;
			_logger = logger;
			_playersService = playersService;
			_dateTimeService = dateTimeService;
		}

		public async Task<Unit> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
		{
			var isUnique = await _playersService.IsNicknameUniqueAsync(request.Nickname);

			if (!isUnique)
			{
				_logger.LogError($"Player with nickname {request.Nickname} already exists");
				throw new EntityExistsException(nameof(Player), request.Nickname);
			}
			
			var player = new Player
			{
				Id = request.Id,
				FirstName = request.FirstName.Trim(),
				LastName = request.LastName.Trim(),
				BirthDate = request.BirthDate,
				Nickname = request.Nickname.Trim(),
				Added = _dateTimeService.Now
			};

			await _context.Players.AddAsync(player, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
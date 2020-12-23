using System;
using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.Services.DateTimeService;
using HorCup.Presentation.Services.IdGenerator;
using HorCup.Presentation.Services.Players;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Players.Commands.AddPlayer
{
	public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, Guid>
	{
		private readonly IHorCupContext _context;
		private readonly ILogger<AddPlayerCommandHandler> _logger;
		private readonly IPlayersService _playersService;
		private readonly IDateTimeService _dateTimeService;
		private readonly IIdGenerator _idGenerator;
		
		public AddPlayerCommandHandler(
			IHorCupContext context,
			ILogger<AddPlayerCommandHandler> logger,
			IPlayersService playersService,
			IDateTimeService dateTimeService,
			IIdGenerator idGenerator)
		{
			_context = context;
			_logger = logger;
			_playersService = playersService;
			_dateTimeService = dateTimeService;
			_idGenerator = idGenerator;
		}

		public async Task<Guid> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
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
				FirstName = request.FirstName.Trim(),
				LastName = request.LastName.Trim(),
				BirthDate = request.BirthDate,
				Nickname = request.Nickname.Trim(),
				Added = _dateTimeService.Now
			};

			await _context.Players.AddAsync(player, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);

			return id;
		}
	}
}
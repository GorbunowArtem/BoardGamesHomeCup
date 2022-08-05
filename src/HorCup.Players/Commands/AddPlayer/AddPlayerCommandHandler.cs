using System;
using System.Threading;
using System.Threading.Tasks;
using HorCup.Infrastructure.Exceptions;
using HorCup.Infrastructure.Services.DateTimeService;
using HorCup.Infrastructure.Services.IdGenerator;
using HorCup.Players.Context;
using HorCup.Players.Models;
using HorCup.Players.Services.Players;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HorCup.Players.Commands.AddPlayer;

public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, Guid>
{
	private readonly IPlayersContext _context;
	private readonly ILogger<AddPlayerCommandHandler> _logger;
	private readonly IPlayersService _playersService;
	private readonly IDateTimeService _dateTimeService;
	private readonly IIdGenerator _idGenerator;
		
	public AddPlayerCommandHandler(
		IPlayersContext context,
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
		var isUnique = await _playersService.IsNicknameUniqueAsync(request.Nickname, null);

		if (!isUnique)
		{
			throw new EntityExistsException(nameof(Player), request.Nickname);
		}

		var id = _idGenerator.NewGuid();
			
		var player = new Player
		{
			Id = id,
			Nickname = request.Nickname.Trim(),
			Added = _dateTimeService.UtcNow
		};

		await _context.Players.AddAsync(player, cancellationToken);

		await _context.SaveChangesAsync(cancellationToken);

		return id;
	}
}
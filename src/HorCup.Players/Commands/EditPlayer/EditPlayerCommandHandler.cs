using System;
using System.Threading;
using System.Threading.Tasks;
using HorCup.Infrastructure.Exceptions;
using HorCup.Players.Context;
using HorCup.Players.Models;
using HorCup.Players.Services.Players;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Players.Commands.EditPlayer
{
	public class EditPlayerCommandHandler : IRequestHandler<EditPlayerCommand, Unit>
	{
		private readonly IPlayersContext _context;
		private readonly ILogger<EditPlayerCommandHandler> _logger;
		private readonly IPlayersService _playersService;

		public EditPlayerCommandHandler(
			IPlayersContext context,
			ILogger<EditPlayerCommandHandler> logger,
			IPlayersService playersService)
		{
			_context = context;
			_logger = logger;
			_playersService = playersService;
		}

		public async Task<Unit> Handle(EditPlayerCommand request, CancellationToken cancellationToken)
		{
			var existing = await _context.Players.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

			_logger.LogInformation($"Trying to get player {request.Id.ToString()}");

			if (existing == null)
			{
				throw new NotFoundException(nameof(Player), request.Id);
			}

			await ValidateNickname();

			existing.Nickname = request.Nickname;

			_context.Players.Update(existing);

			_logger.LogInformation($"Updating player {request.Id.ToString()}");

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;

			async Task ValidateNickname()
			{
				if (!string.Equals(existing.Nickname, request.Nickname, StringComparison.OrdinalIgnoreCase))
				{
					var isNicknameUnique = await _playersService.IsNicknameUniqueAsync(request.Nickname, request.Id);

					_logger.LogInformation(
						$"Checking if nickname {request.Nickname} for {request.Id.ToString()} is unique ");

					if (!isNicknameUnique)
					{
						throw new EntityExistsException(nameof(Player), request.Nickname);
					}
				}
			}
		}
	}
}
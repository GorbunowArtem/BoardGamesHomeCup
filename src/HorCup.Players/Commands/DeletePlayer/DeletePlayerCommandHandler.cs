using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HorCup.Infrastructure.Exceptions;
using HorCup.Players.Context;
using HorCup.Players.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HorCup.Players.Commands.DeletePlayer
{
	public class DeletePlayerCommandHandler: IRequestHandler<DeletePlayerCommand, Unit>
	{
		private readonly IPlayersContext _context;
		private readonly ILogger<DeletePlayerCommandHandler> _logger;

		public DeletePlayerCommandHandler(IPlayersContext context, ILogger<DeletePlayerCommandHandler> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<Unit> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
		{
			var player = _context.Players.FirstOrDefault(p => p.Id == request.Id);

			if (player == null)
			{
				_logger.LogError($"Entity with id {request.Id} not found. Unable to delete");
				throw new NotFoundException(nameof(Player), request.Id);
			}
			
			_logger.LogInformation($"Deleting {nameof(Player)} with id {request.Id}");
			
			_context.Players.Remove(player);

			await _context.SaveChangesAsync(cancellationToken);
			
			return Unit.Value;
		}
	}
}
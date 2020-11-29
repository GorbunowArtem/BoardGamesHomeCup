using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using HorCup.Presentation.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Players.Commands.DeletePlayer
{
	public class DeletePlayerCommandHandler: IRequestHandler<DeletePlayerCommand, Unit>
	{
		private readonly IHorCupContext _context;
		private readonly ILogger<DeletePlayerCommandHandler> _logger;

		public DeletePlayerCommandHandler(IHorCupContext context, ILogger<DeletePlayerCommandHandler> logger)
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
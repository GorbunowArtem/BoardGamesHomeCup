using System.Threading;
using System.Threading.Tasks;
using HorCup.Infrastructure.Exceptions;
using HorCup.Presentation.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Games.Commands.DeleteGame
{
	public class DeleteGameCommandHandler: IRequestHandler<DeleteGameCommand, Unit>
	{
		private readonly IHorCupContext _context;
		private readonly ILogger<DeleteGameCommandHandler> _logger;

		public DeleteGameCommandHandler(IHorCupContext context, ILogger<DeleteGameCommandHandler> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<Unit> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
		{
			var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

			if (game == null)
			{
				_logger.LogError($"Game with id {request.Id} was not found");
				throw new NotFoundException(nameof(Game), request.Id);
			}
			
			_logger.LogInformation($"Deleting game with id {request.Id.ToString()}");

			_context.Games.Remove(game);

			await _context.SaveChangesAsync(cancellationToken);
			
			return Unit.Value;
		}
	}
}
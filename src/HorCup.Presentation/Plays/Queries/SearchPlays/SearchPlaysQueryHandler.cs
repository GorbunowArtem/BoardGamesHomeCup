using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Plays.Queries.SearchPlays
{
	public class SearchPlaysQueryHandler: IRequestHandler<SearchPlaysQuery, (IEnumerable<PlayViewModel> items, int total)>
	{
		private readonly IHorCupContext _context;
		private readonly ILogger<SearchPlaysQueryHandler> _logger;

		public SearchPlaysQueryHandler(IHorCupContext context, ILogger<SearchPlaysQueryHandler> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<(IEnumerable<PlayViewModel> items, int total)> Handle(SearchPlaysQuery request, CancellationToken cancellationToken)
		{
			var playsQuery = _context.Plays.AsQueryable();
			
			var plays = await playsQuery
				.Include(p => p.Game)
				.Include(p => p.PlayerScores)
				.ThenInclude(p => p.Player)
				.Select(p => new PlayViewModel
				{
					Id = p.Id,
					Notes = p.Notes,
					GameTitle = p.Game.Title,
					GameId = p.GameId,
					PlayedDate = p.PlayedDate,
					PlayerScores = p.PlayerScores.Select(ps => new PlayScoreViewModel(
						new IdName(
							ps.PlayerId,
							$"{ps.Player.FirstName} {ps.Player.LastName}"),
							ps.Score,
						 ps.IsWinner
					))  
				})
				.OrderByDescending(pl => pl.PlayedDate)
				.Take(request.Take)
				.Skip(request.Skip)
				.ToListAsync(cancellationToken);

			var total = await playsQuery.CountAsync(cancellationToken);
			
			return (plays, total);
		}
	}
}
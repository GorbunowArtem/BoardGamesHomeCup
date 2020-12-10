using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HorCup.Presentation.Context;
using HorCup.Presentation.PlayScores;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Presentation.Plays.Queries.SearchPlays
{
	public class SearchPlaysQueryHandler: IRequestHandler<SearchPlaysQuery, (IEnumerable<PlayViewModel> items, int total)>
	{
		private readonly IMapper _mapper;
		private readonly IHorCupContext _context;

		public SearchPlaysQueryHandler(IMapper mapper, IHorCupContext context)
		{
			_mapper = mapper;
			_context = context;
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
					PlayerScores = p.PlayerScores.Select(ps => new PlayScoreViewModel
					{
						Score = ps.Score,
						IsWinner = ps.IsWinner,
						Player = new IdName(ps.PlayerId, ps.Player.Nickname),
					})  
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
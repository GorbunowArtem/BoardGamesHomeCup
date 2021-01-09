using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using HorCup.Presentation.Services.Games;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Games.Queries.GetDetails
{
	public class GetGameDetailsQueryHandler : IRequestHandler<GetGameDetailsQuery, GameDetailsViewModel>
	{
		private readonly IHorCupContext _context;
		private readonly ILogger<GetGameDetailsQueryHandler> _logger;
		private readonly IGamesService _gamesService;

		public GetGameDetailsQueryHandler(
			IHorCupContext context,
			ILogger<GetGameDetailsQueryHandler> logger,
			IGamesService gamesService)
		{
			_context = context;
			_logger = logger;
			_gamesService = gamesService;
		}

		public async Task<GameDetailsViewModel> Handle(GetGameDetailsQuery request, CancellationToken cancellationToken)
		{
			var game = await _gamesService.TryGetGameAsync(request.Id, cancellationToken);

			var gameStats =
				await _context.GamesStatistics.FirstOrDefaultAsync(gs => gs.GameId == request.Id, cancellationToken);

			if (gameStats != null)
			{
				return new GameDetailsViewModel
				{
					Id = game.Id,
					Title = game.Title,
					MaxPlayers = game.MaxPlayers,
					MinPlayers = game.MinPlayers,
					AverageScore = gameStats.AverageScore,
					TimesPlayed = gameStats.TimesPlayed,
					LastPlayedDate = gameStats.LastPlayedDate
				};
			}

			return new GameDetailsViewModel
			{
				Id = game.Id,
				Title = game.Title,
				MaxPlayers = game.MaxPlayers,
				MinPlayers = game.MinPlayers,
				AverageScore = 0d,
				TimesPlayed = 0,
				LastPlayedDate = null
			};
		}
	}
}
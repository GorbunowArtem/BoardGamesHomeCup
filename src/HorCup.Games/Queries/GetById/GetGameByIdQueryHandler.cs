using System.Threading;
using System.Threading.Tasks;
using Revo.Core.Commands;
using Revo.DataAccess.Entities;

namespace HorCup.Games.Queries.GetById
{
	public class GetGameByIdQueryHandler: IQueryHandler<GetGameByIdQuery, GameReadModel>
	{
		private readonly IReadRepository _repository;

		public GetGameByIdQueryHandler(IReadRepository repository)
		{
			_repository = repository;
		}

		public async Task<GameReadModel> HandleAsync(GetGameByIdQuery query, CancellationToken cancellationToken)
		{
			var game = await _repository.GetAsync<GameReadModel>(cancellationToken, query.Id);
			
			return game;
		}
	}
}
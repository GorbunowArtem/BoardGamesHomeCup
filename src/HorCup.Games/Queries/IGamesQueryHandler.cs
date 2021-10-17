using System;
using System.Threading.Tasks;

namespace HorCup.Games.Queries
{
	public interface IGamesQueryHandler
	{
		Task<GameProjection> Get(Guid id);
	}
}
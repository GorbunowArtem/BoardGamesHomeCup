using Revo.DataAccess.Entities;
using Revo.Domain.ReadModel;

namespace HorCup.Games.Queries
{
	public class GameReadModel : EntityReadModel
	{
		public string Title { get; set; }

		public int MinPlayers { get; set; }

		public int MaxPlayers { get; set; }

		public string Description { get; set; }
	}
}
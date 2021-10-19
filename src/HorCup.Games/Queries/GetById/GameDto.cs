using Revo.Domain.Dto;

namespace HorCup.Games.Queries.GetById
{
	public class GameDto: EntityDto
	{
		public string Title { get; set; }

		public int MinPlayers { get; set; }

		public int MaxPlayers { get; set; }

		public string Description { get; set; }
	}
}
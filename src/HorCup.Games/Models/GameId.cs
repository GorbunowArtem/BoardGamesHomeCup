using Akkatecture.Core;

namespace HorCup.Games.Models
{
	public class GameId: Identity<GameId>
	{
		public GameId(string value) : base(value)
		{
		}
	}
}
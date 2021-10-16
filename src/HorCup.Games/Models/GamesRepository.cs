using System.Reflection.Metadata;
using Akka.Actor;
using HorCup.Games.Events;

namespace HorCup.Games.Models
{
	public class GamesRepository: ReceiveActor
	{
		public GamesRepository()
		{
			Receive<GameTitleChanged>(Handle);
		}

		private bool Handle(GameTitleChanged arg)
		{
			throw new System.NotImplementedException();
		}
	}
}
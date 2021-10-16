using Akkatecture.Akka;

namespace HorCup.Games.Models
{
	public class GameActorService : IGameActorService
	{
		private readonly ActorRefProvider<GameManager> _gameManager;

		public GameActorService(ActorRefProvider<GameManager> gameManager)
		{
			_gameManager = gameManager;
		}
	}
}
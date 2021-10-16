using Akkatecture.Aggregates;
using Akkatecture.Specifications.Provided;
using HorCup.Games.Commands.AddGame;
using HorCup.Games.Events;

namespace HorCup.Games.Models
{
	public class GameAggregate : AggregateRoot<GameAggregate, GameId, GameState>,
		IExecute<AddGameCommand>
	{
		// public string Title { get; set; }
		//
		// public int MaxPlayers { get; set; }
		//
		// public int MinPlayers { get; set; }
		//
		// public DateTime Added { get; set; }

		public GameAggregate(GameId id) : base(id)
		{
		}

		public bool Execute(AddGameCommand command)
		{
			var isNewSpec = new AggregateIsNewSpecification();

			if (isNewSpec.IsSatisfiedBy(this))
			{
				// var playersAmountSpec = new ValidPlayersAmountSpecification();
				// var titleSpecification = new ValidTitleSpecification();
				//
				// var andSpec = playersAmountSpec.And(titleSpecification);

				// if (andSpec.IsSatisfiedBy(State))
				// {
					var titleChanged = new GameTitleChanged(command.Title);

					var minPlayersChanged = new GameMinPlayersChanged(command.MinPlayers);

					var maxPlayersChanged = new GameMaxPlayersChanged(command.MaxPlayers);

					Emit(titleChanged);
					Emit(minPlayersChanged);
					Emit(maxPlayersChanged);
					// EmitAll(titleChanged, minPlayersChanged, maxPlayersChanged);
				// }
			}

			return true;
		}
	}
}
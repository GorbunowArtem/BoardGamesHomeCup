using HorCup.Infrastructure.Events;

namespace HorCup.Infrastructure.EventBus
{
	public interface IEventBus
	{
		void Publish(IntegrationEvent @event);

		void Subscribe<TEvent, TEventHandler>()
			where TEvent : IntegrationEvent
			where TEventHandler : IEventHandler<TEvent>;
	}
}
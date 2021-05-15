using System.Threading.Tasks;

namespace HorCup.Infrastructure.Events
{
	public interface IEventHandler<in TEvent> where TEvent : IntegrationEvent
	{
		Task Handle(TEvent @event);
	}
}
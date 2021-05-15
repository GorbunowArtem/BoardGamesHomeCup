using System.Text;
using System.Text.Json;
using HorCup.Infrastructure.Events;
using RabbitMQ.Client;

namespace HorCup.Infrastructure.EventBus.RabbitMQEventBus
{
	public class RabbitEventBus : IEventBus
	{
		public void Publish(IntegrationEvent @event)
		{
			var factory = new ConnectionFactory()
			{
				HostName = "localhost"
			};

			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();
			
			channel.QueueDeclare("Statistic");
					
			channel.BasicPublish(string.Empty, 
				"Statistic",
				body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event)));
		}

		public void Subscribe<TEvent, TEventHandler>() where TEvent : IntegrationEvent
			where TEventHandler : IEventHandler<TEvent>
		{
			throw new System.NotImplementedException();
		}
	}
}
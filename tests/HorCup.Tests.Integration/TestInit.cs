using BoDi;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Configuration.JsonConfig;

namespace HorCup.Tests.Integration
{
	[Binding]
	public class TestInit
	{
		private readonly IObjectContainer _container;
		private static IConfiguration _configuration;

		public TestInit(IObjectContainer container)
		{
			_container = container;
		}

		[BeforeScenario]
		public void CreateConfig()
		{
			if (_configuration == null)
			{
				_configuration = new ConfigurationBuilder()
					.AddJsonFile("appsettings.json")
					.Build();
			}

			_container.RegisterInstanceAs(_configuration, typeof(IConfiguration));
		}
	}
}
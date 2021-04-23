using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;

namespace HorCup.Tests.Integration.Drivers
{
	public class WebDriver
	{
		private readonly HttpClient _httpClient;
		private HttpResponseMessage _httpResponse;

		public WebDriver()
		{
			_httpClient = new HttpClient();
		}

		public async Task HttpGet(string url) => _httpResponse = await _httpClient.GetAsync(url);

		public async Task<string> HttpPost<T>(string url, T body)
		{
			_httpResponse = await _httpClient.PostAsync(url,
				new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
			
			return await _httpResponse.Content.ReadAsStringAsync();
		}
		
		public void CheckResponseStatusCode(int expectedStatusCode)
		{
			_httpResponse.StatusCode.Should().Be(expectedStatusCode);
		}
	}
}
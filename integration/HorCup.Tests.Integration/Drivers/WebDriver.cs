using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace HorCup.Tests.Integration.Drivers
{
	public class WebDriver
	{
		private readonly IConfiguration _configuration;
		private readonly HttpClient _httpClient;
		private HttpResponseMessage _httpResponse;

		public WebDriver(IConfiguration configuration)
		{
			_configuration = configuration;
			_httpClient = new HttpClient {BaseAddress = new Uri(configuration["BaseUrl"])};
		}

		public async Task<T> GetAsync<T>(string url)  where T: class
		{
			_httpResponse = await _httpClient.GetAsync(url);

			var response = await _httpResponse.Content.ReadAsStringAsync(default);

			if (string.IsNullOrEmpty(response))
			{
				return null;
			}
			
			return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
		}

		public async Task<string> PostAsync<T>(string endpoint, T body)
		{
			_httpResponse = await _httpClient.PostAsync(endpoint,
				new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));

			return await _httpResponse.Content.ReadAsStringAsync(default);
		}

		public async Task PatchAsync<T>(string endpoint, T body)
		{
			_httpResponse = await _httpClient.PatchAsync(endpoint,
				new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
		}

		public async Task DeleteAsync(string url)
		{
			_httpResponse = await _httpClient.DeleteAsync(url);
		}

		public void CheckResponseStatusCode(int expectedStatusCode)
		{
			_httpResponse.StatusCode.Should().Be(expectedStatusCode);
		}
	}
}
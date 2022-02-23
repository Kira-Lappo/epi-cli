using System.Net;
using System.Net.Http.Headers;

namespace OptimizelyApiClient.Cli.Client;

public partial class ServerApiClient : IDisposable
{
	private const string CommerceBaseUrl = "/episerverapi/commerce";

	private readonly HttpClient _client;
	private readonly Dictionary<string, string> _tokenFormParams;

	public ServerApiClient(ServerApiOptions options)
	{
		_client  = CreateClient(options);
		_tokenFormParams = BuildGetTokenFormParameters(options);
	}

	public async Task RefreshTokenAsync()
	{
		var token = await GetTokenAsync();
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
	}

	public void Dispose()
	{
		_client?.Dispose();
	}

	private static HttpClient CreateClient(ServerApiOptions options)
	{
		var client = new HttpClient();
		client.BaseAddress = options.BaseAddress;
		return client;
	}

	private async Task<string> GetTokenAsync()
	{
		const string refreshRoute = "/api/episerver/connect/token";

		var tokenRequestContent = new FormUrlEncodedContent(_tokenFormParams);
		var response = await _client.PostAsync(refreshRoute, tokenRequestContent);
		if (response.StatusCode != HttpStatusCode.OK)
		{
			throw new InvalidOperationException("Can't retrieve token from server");
		}

		var responseContent = await response.Content.ReadAsStringAsync();
		var token = responseContent.ConvertFromJsonNode<string>("access_token");
		return token;
	}

	private static Dictionary<string, string> BuildGetTokenFormParameters(ServerApiOptions options)
	{
		return new Dictionary<string, string>
		{
			{ "client_id", options.ClientId },
			{ "client_secret", options.ClientSecret },
			{ "grant_type", "password" },
			{ "username", options.UserName },
			{ "password", options.Password }
		};
	}
}

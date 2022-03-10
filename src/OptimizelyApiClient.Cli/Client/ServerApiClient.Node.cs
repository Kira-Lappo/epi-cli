using OptimizelyApiClient.Cli.Client.Catalogs;

namespace OptimizelyApiClient.Cli.Client;

public partial class ServerApiClient : INodeClient
{
	private const string NodeBaseUrl = $"{CommerceBaseUrl}/nodes";

	public async Task<List<CatalogNode>> GetNodesAsync(string catalogName)
	{
		var path = $"{CommerceBaseUrl}/catalog/{catalogName}/nodes";
		var response = await _client.GetAsync(path);
		response.EnsureSuccessStatusCode();
		var content = await response.Content.ReadAsStringAsync();
		var model = content.ConvertFromJson<List<CatalogNode>>();
		return model;
	}

	private static string GetNodeUrl(string nodeCode)
	{
		return $"{NodeBaseUrl}/{nodeCode}";
	}
}

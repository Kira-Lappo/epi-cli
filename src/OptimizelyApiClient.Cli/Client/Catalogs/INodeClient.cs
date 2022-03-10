namespace OptimizelyApiClient.Cli.Client.Catalogs;

public interface INodeClient
{
	Task<List<CatalogNode>> GetNodesAsync(string catalogName);
}

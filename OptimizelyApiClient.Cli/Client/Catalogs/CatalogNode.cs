namespace OptimizelyApiClient.Cli.Client.Catalogs;

public class CatalogNode
{
	public bool IsActive { get; set; }

	public string Name { get; set; }

	public string Code { get; set; }

	public List<ResourceLink> Children { get; set; }

	public List<ResourceLink> Entries { get; set; }
}

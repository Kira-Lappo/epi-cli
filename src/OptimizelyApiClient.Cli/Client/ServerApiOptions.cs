using System.Runtime.Serialization;

namespace OptimizelyApiClient.Cli.Client;

public class ServerApiOptions
{
	[DataMember(Name = "baseUri")]
	public Uri BaseUri { get; set; }

	[DataMember(Name = "clientId")]
	public string ClientId { get; set; }

	[DataMember(Name = "clientSecret")]
	public string ClientSecret { get; set; }

	public static ServerApiOptions Default => new()
	{
		BaseUri = new Uri("http://localhost:8080"),
		ClientId = "cli",
		ClientSecret = "clis3cr3t!",
	};
}

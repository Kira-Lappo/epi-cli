namespace OptimizelyApiClient.Cli.Client;

public static class HttpUtils
{
	public static HttpContent EmptyContent { get; } = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>());
}

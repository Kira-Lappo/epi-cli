namespace OptimizelyApiClient.Cli.Client;

public static class ResponseExtensions
{
	public static async Task<Guid> ReadGuidAsync(this HttpResponseMessage response)
	{
		var guidString = await response.Content.ReadAsStringAsync();
		guidString = guidString.Replace("\"", "");

		return Guid.TryParse(guidString, out var taskId) ? taskId : Guid.Empty;
	}
}

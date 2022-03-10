using OptimizelyApiClient.Cli.Client.Jobs;

namespace OptimizelyApiClient.Cli.Client;

public partial class ServerApiClient : IJobStatusClient
{
	private const string GetJobBaseRoute = $"{CommerceBaseUrl}/task";

	public async Task<JobMessage> GetJobStatusAsync(Guid jobId)
	{
		var path = $"{GetJobRoute(jobId)}/status";
		var response = await _client.GetAsync(path);
		response.EnsureSuccessStatusCode();
		var content = await response.Content.ReadAsStringAsync();
		var status = content.ConvertFromJson<JobMessage>();
		return status;
	}

	private static string GetJobRoute(Guid jobId)
	{
		return $"{GetJobBaseRoute}/{jobId:N}";
	}
}

namespace OptimizelyApiClient.Cli.Client.Jobs;

public interface IJobStatusClient
{
	Task<JobMessage> GetJobStatusAsync(Guid jobId);
}

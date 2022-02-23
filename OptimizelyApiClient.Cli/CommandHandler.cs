using OptimizelyApiClient.Cli.Client;
using OptimizelyApiClient.Cli.Client.Jobs;
using Serilog;

namespace OptimizelyApiClient.Cli;

public class CommandHandler
{
	private readonly ServerApiClient _client;
	private readonly ILogger _logger;

	public CommandHandler(ServerApiClient client, ILogger logger)
	{
		_client = client;
		_logger      = logger;
	}

	public async Task ImportEpiServerDataAsync(FileInfo dataFile)
	{
		try
		{
			await DoImportAsync(dataFile);
		}
		catch (Exception e)
		{
			_logger.Error(e, "Error ar importing episerverdata file");
		}
	}

	private async Task DoImportAsync(FileInfo dataFile)
	{
		_logger.Information("Uploading file {FileName} to server...", dataFile.Name);

		var jobId = await _client.UploadEpiServerDataFileAsync(dataFile);

		_logger.Information(
			"File {FileName} is uploaded, watching the state of import (JobId: {JobId})",
			dataFile.Name,
			jobId);

		while (true)
		{
			var jobStatus = await _client.GetJobStatusAsync(jobId);
			_logger.Information("Status: {@JobStatus}", jobStatus);
			if (jobStatus.MessageType.IsFinished())
			{
				break;
			}
		}
	}

	public async Task GetAllNodesAsync(string name)
	{
		var nodes = await _client.GetNodesAsync(name);
		_logger.Information("{@Nodes}", nodes);
	}
}

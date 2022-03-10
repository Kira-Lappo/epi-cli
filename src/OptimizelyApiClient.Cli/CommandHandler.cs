using System.Diagnostics;
using OptimizelyApiClient.Cli.Client;
using OptimizelyApiClient.Cli.Client.Jobs;
using Serilog;
using Serilog.Context;

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

	public async Task ImportChunkedAsync(FileInfo dataFile, int? maxChunkSize, bool isAgr)
	{
		await DoUploadAsync(dataFile, async () =>
		{
			var uploadId = await _client.UploadChunksAsync(dataFile, maxChunkSize);
			if (isAgr)
			{
				return await _client.UploadFileToAccessGlobalRootAsync(uploadId);
			}

			return await _client.UploadFileToSiteAsync(uploadId);
		});
	}

	public async Task ImportAsync(FileInfo dataFile, bool uploadToAgr = false)
	{
		await DoUploadAsync(dataFile, () =>
		{
			if (uploadToAgr)
			{
				return _client.UploadFileToAccessGlobalRootAsync(dataFile);
			}

			return _client.UploadFileAsync(dataFile);
		});
	}

	public async Task FollowJobAsync(Guid jobId)
	{
		using var jobContext = LogContext.PushProperty("JobId", jobId);

		_logger.Information("Start watching job (JobId: {JobId})", jobId);

		var stopwatch = Stopwatch.StartNew();

		JobMessage currentMessage;
		while (true)
		{
			currentMessage = await _client.GetJobStatusAsync(jobId);
			_logger.Information("Status: {@JobStatus}", currentMessage);
			if (currentMessage.IsFinished())
			{
				break;
			}

			await Task.Delay(10000);
		}

		_logger.Information(
			"Job stopped with status {UploadStatus} after {SpentTime:g}. {@LastMessage}",
			currentMessage.MessageType,
			stopwatch.Elapsed,
			currentMessage);
	}

	private async Task DoUploadAsync(FileInfo dataFile, Func<Task<Guid>> startUploadAsync)
	{
		using var fileContext = LogContext.PushProperty("FileName", dataFile.FullName);

		_logger.Information("Uploading file {FileName} to server...", dataFile.Name);

		var jobId = await startUploadAsync();

		_logger.Information("File {FileName} is uploaded", dataFile.Name);

		await FollowJobAsync(jobId);

		_logger.Information("File {FileName} has been imported", dataFile.Name);
	}

	public async Task GetAllNodesAsync(string name)
	{
		var nodes = await _client.GetNodesAsync(name);
		_logger.Information("{@Nodes}", nodes);
	}
}

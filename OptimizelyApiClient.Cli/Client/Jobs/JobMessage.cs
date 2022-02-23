namespace OptimizelyApiClient.Cli.Client.Jobs;

public record JobMessage
{
	public DateTime TimestampUtc { get; set; }
	public MessageType MessageType { get; set; }
	public string Message { get; set; }
	public string StageName { get; set; }
	public int StageIndex { get; set; }
	public int StageCount { get; set; }
	public int StageProgress { get; set; }
	public int StageTotalProgress { get; set; }
	public string ExceptionMessage { get; set; }
	public string ExceptionStackTrace { get; set; }
}

namespace OptimizelyApiClient.Cli.Client.Upload;

[Serializable]
public class UploadFile
{
	public Guid UploadId { get; set; }

	public long OffSet { get; set; }

	public DateTime Expires { get; set; }
}

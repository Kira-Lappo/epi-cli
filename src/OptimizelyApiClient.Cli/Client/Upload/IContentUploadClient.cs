namespace OptimizelyApiClient.Cli.Client.Upload;

public interface IContentUploadClient
{
	Task<Guid> UploadFileAsync(FileInfo fileInfo);
}

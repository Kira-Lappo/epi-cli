namespace OptimizelyApiClient.Cli.Client.Contents;

public interface IContentUploadClient
{
	Task<Guid> UploadEpiServerDataFileAsync(FileInfo fileInfo);
}

using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using OptimizelyApiClient.Cli.Client.Upload;

namespace OptimizelyApiClient.Cli.Client;

public partial class ServerApiClient
{
	private const string UploadBaseRoute = $"{CommerceBaseUrl}/import/upload";
	private const string UploadChunkRoute = $"{UploadBaseRoute}/chunk";
	private const string UploadChunkCommitRoute = $"{UploadBaseRoute}/commit";

	private const int DefaultChunkMaxSize = 100 * 1024 * 1024;

	public async Task<Guid> UploadChunksAsync(FileInfo fileInfo, int? maxChunkSize)
	{
		var chunkSize = maxChunkSize ?? DefaultChunkMaxSize;
		var fileName = fileInfo.Name;

		_logger.Debug(
			"Uploading chunks of max size {ChunkMaxSize} bytes ({ChunkMaxSizeMB} MB) for file {FileName} of size {FileSize} bytes ({FileSizeMB} MB)",
			chunkSize,
			chunkSize / ( 1024 * 1024 ),
			fileInfo.FullName,
			fileInfo.Length,
			fileInfo.Length / ( 1024 * 1024 ));

		await using var fileStream = fileInfo.OpenRead();

		UploadFile uploadFile = null;
		long nextChunkOffset = 0;
		foreach (var (bytes, length) in fileStream.GetChunks(chunkSize))
		{
			using var chunkReader = new StreamContent(new MemoryStream(bytes, 0, length));
			using var content = new MultipartFormDataContent();
			content.Add(chunkReader, "file", fileName);

			var route = uploadFile == null
				? GetUploadChunkRoute()
				: GetUploadChunkRoute(uploadFile.UploadId, nextChunkOffset);

			nextChunkOffset += length;

			var response = await _client.PutAsync(route, content);
			response.EnsureSuccessStatusCode();
			uploadFile = await response.Content.ReadFromJsonAsync<UploadFile>();

			_logger.Debug("Uploaded chunk of size {ChunkSize} UploadId: {UploadId}", length, uploadFile?.UploadId);
		}

		if (uploadFile == null)
		{
			throw new InvalidOperationException("File {FileName} is empty");
		}

		var commitRoute = GetCommitUploadChunkRoute(uploadFile.UploadId);
		var commitResponse = await _client.PostAsync(commitRoute, HttpUtils.EmptyContent);
		commitResponse.EnsureSuccessStatusCode();
		var uploadId = await commitResponse.ReadGuidAsync();

		_logger.Debug("Committed uploaded chunks for upload id {UploadId}", uploadId);

		return uploadId;
	}

	private static string GetUploadChunkRoute(UploadFile uploadFile = null)
	{
		if (uploadFile == null)
		{
			return UploadChunkRoute;
		}

		return GetUploadChunkRoute(uploadFile.UploadId, uploadFile.OffSet + 1);
	}

	private static string GetUploadChunkRoute(Guid uploadId, long offset = 0)
	{
		if (offset == default)
		{
			return $"{UploadChunkRoute}/{uploadId}";
		}

		return $"{UploadChunkRoute}/{uploadId}/{offset}";
	}

	private static string GetCommitUploadChunkRoute(Guid uploadId)
	{
		return $"{UploadChunkCommitRoute}/{uploadId}";
	}
}

using System.Net;
using OptimizelyApiClient.Cli.Client.Upload;

namespace OptimizelyApiClient.Cli.Client;

public partial class ServerApiClient : IContentUploadClient
{
	private const string ImportCmsBaseRoute = $"{CommerceBaseUrl}/import/cms";
	private const string ImportSiteBaseRoute = $"{ImportCmsBaseRoute}/site";
	private const string ImportAgrBaseRoute = $"{ImportCmsBaseRoute}/assetglobalroot";

	public async Task<Guid> UploadFileToAccessGlobalRootAsync(Guid uploadId)
	{
		var path = GetImportArgRoute(uploadId);
		var response = await _client.PostAsync(path, HttpUtils.EmptyContent);
		response.EnsureSuccessStatusCode();

		return await response.ReadGuidAsync();
	}

	public async Task<Guid> UploadFileToSiteAsync(Guid uploadId)
	{
		var path = GetImportSiteRoute(uploadId);
		var response = await _client.PostAsync(path, HttpUtils.EmptyContent);
		response.EnsureSuccessStatusCode();

		return await response.ReadGuidAsync();
	}

	public async Task<Guid> UploadFileAsync(FileInfo fileInfo)
	{
		if (fileInfo == null)
		{
			throw new ArgumentNullException(nameof(fileInfo));
		}

		if (!fileInfo.Exists)
		{
			throw new FileNotFoundException("Can't find Epi Server Data file", fileInfo.FullName);
		}

		var content = new MultipartFormDataContent();
		await using var filestream = fileInfo.OpenRead();
		var contentName = fileInfo.Name;

		content.Add(new StreamContent(filestream), "file", contentName);

		const string path = $"{ImportSiteBaseRoute}/localhost/localhost:8080/en-us";
		var response = await _client.PostAsync(path, content);
		response.EnsureSuccessStatusCode();

		return await response.ReadGuidAsync();
	}

	public async Task<Guid> UploadFileToAccessGlobalRootAsync(FileInfo fileInfo)
	{
		if (fileInfo == null)
		{
			throw new ArgumentNullException(nameof(fileInfo));
		}

		if (!fileInfo.Exists)
		{
			throw new FileNotFoundException("Can't file Epi Server Data file", fileInfo.FullName);
		}

		var content = new MultipartFormDataContent();
		await using var filestream = fileInfo.OpenRead();
		var contentName = fileInfo.Name;

		content.Add(new StreamContent(filestream), "file", contentName);

		var path = ImportAgrBaseRoute;
		var response = await _client.PostAsync(path, content);
		response.EnsureSuccessStatusCode();

		return await response.ReadGuidAsync();
	}

	private string GetImportArgRoute(Guid uploadId)
	{
		return $"{ImportAgrBaseRoute}/{uploadId:N}";
	}

	private string GetImportSiteRoute(Guid uploadId)
	{
		return $"{ImportSiteBaseRoute}/{uploadId:N}";
	}
}

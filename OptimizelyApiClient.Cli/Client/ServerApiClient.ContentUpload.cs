using System.Net;
using OptimizelyApiClient.Cli.Client.Contents;

namespace OptimizelyApiClient.Cli.Client;

public partial class ServerApiClient : IContentUploadClient
{
	private const string CommerceImportEpiServerDataBaseRoute = "/episerverapi/commerce/import/cms/site";

	public async Task<Guid> UploadEpiServerDataFileAsync(FileInfo fileInfo)
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

		var path = $"{CommerceImportEpiServerDataBaseRoute}/localhost/localhost/en-us";
		var response = await _client.PostAsync(path, content);
		if (response.StatusCode != HttpStatusCode.OK)
		{
			// FixMe
			throw new InvalidOperationException(response.ReasonPhrase);
		}

		return await response.ReadGuidAsync();
	}
}

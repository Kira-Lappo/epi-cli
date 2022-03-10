using System.CommandLine;

namespace OptimizelyApiClient.Cli;

public partial class CommandFactory
{
	private Command CreateImportCommandArea()
	{
		var command = new Command("import")
		{
			CreateEpiDataImportCommand(),
		};

		return command;
	}

	private Command CreateEpiDataImportCommand()
	{
		var command = new Command("epidata");

		var filePath = new Option<FileInfo>("--file", "The path to EPISEVERDATA file.")
		{
			IsRequired = true
		};

		filePath.AddAlias("-f");
		command.AddOption(filePath);

		var agr = new Option<bool>("--agr", "Import file to Access Global Root");
		command.AddOption(agr);

		var useChunks = new Option<bool>("--use-chunks", "Use chunked upload for big files");
		command.AddOption(useChunks);

		var maxChunkSize = new Option<int?>("--chunk-size", "Maximum size of chunk");
		maxChunkSize.SetDefaultValue(50 * 1024 * 1024);
		command.AddOption(maxChunkSize);

		command.SetAsyncHandler(
			async (FileInfo fi, bool isAgr, bool useChunks, int? maxChunkSize) =>
			{
				if (useChunks)
				{
					await _handler.ImportChunkedAsync(fi, maxChunkSize, isAgr);
					return;
				}

				await _handler.ImportAsync(fi, isAgr);
			},
			filePath, agr, useChunks, maxChunkSize);

		return command;
	}
}

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

		var filePath = new Argument<FileInfo>("The path to EPISEVERDATA file.");
		command.AddArgument(filePath);

		command.SetAsyncHandler(
			(FileInfo fi) => _handler.ImportEpiServerDataAsync(fi),
			filePath);

		return command;
	}
}

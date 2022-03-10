using System.CommandLine;

namespace OptimizelyApiClient.Cli;

public partial class CommandFactory
{
	private Command CreateCatalogCommandArea()
	{
		var command = new Command("catalog")
		{
			CreateGetCatalogCommand(),
		};

		return command;
	}

	private Command CreateGetCatalogCommand()
	{
		var command = new Command("ls");
		command.AddAlias("list");

		var catalogName = new Argument<string>("Catalog name.");
		command.AddArgument(catalogName);

		command.SetAsyncHandler(
			(string name) => _handler.GetAllNodesAsync(name),
			catalogName);

		return command;
	}
}

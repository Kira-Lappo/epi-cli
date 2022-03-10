using System.CommandLine;

namespace OptimizelyApiClient.Cli;

public partial class CommandFactory
{
	private readonly CommandHandler _handler;

	public CommandFactory(CommandHandler handler)
	{
		_handler = handler;
	}

	public RootCommand Build()
	{
		var rootCommand = new RootCommand("Simple CLI for Optimizely Server API with minimal functionality.")
		{
			CreateImportCommandArea(),
			CreateCatalogCommandArea(),
			CreateJobCommandArea(),
		};

		// to support IConfiguration setups
		rootCommand.TreatUnmatchedTokensAsErrors = false;

		return rootCommand;
	}
}

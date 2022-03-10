using Microsoft.Extensions.Configuration;
using OptimizelyApiClient.Cli.Client;
using System.CommandLine;
using OptimizelyApiClient.Cli;
using Serilog;
using Serilog.Events;

var configuration = new ConfigurationBuilder()
	.AddEnvironmentVariables("EPICLI_")
	.AddJsonFile($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.config/epicli/config.json", optional: true)
	.AddJsonFile("epicli.json", optional: true)
	.AddCommandLine(args)
	.Build();

var loggerBuilder = new LoggerConfiguration()
	.MinimumLevel.Debug()
	.ReadFrom.Configuration(configuration)
	.WriteTo.Console(LogEventLevel.Debug);

var globalLogger = loggerBuilder.CreateLogger();
var logger = globalLogger.ForContext<Program>();

var options = ServerApiOptions.Default;
configuration.GetSection("api").Bind(options);

logger.Information("Epi cli. Host: {BaseUri} ", options.BaseUri);

using var client = new ServerApiClient(options, globalLogger.ForContext<ServerApiClient>());
await client.RefreshTokenAsync();

var commandHandler = new CommandHandler(client, globalLogger.ForContext<CommandHandler>());

var rootCommand = new CommandFactory(commandHandler).Build();
return await rootCommand.InvokeAsync(args);

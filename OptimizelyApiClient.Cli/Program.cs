using Microsoft.Extensions.Configuration;
using OptimizelyApiClient.Cli.Client;
using System.CommandLine;
using OptimizelyApiClient.Cli;
using Serilog;

var configuration = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json", optional: true)
	.AddCommandLine(args)
	.Build();

var logger = new LoggerConfiguration()
	.WriteTo.Console()
	.CreateLogger();

var options = configuration.GetSection("ServerApi").Get<ServerApiOptions>();
using var client = new ServerApiClient(options);
await client.RefreshTokenAsync();

var commandHandler = new CommandHandler(client, logger);

var rootCommand = new CommandFactory(commandHandler).Build();
return await rootCommand.InvokeAsync(args);

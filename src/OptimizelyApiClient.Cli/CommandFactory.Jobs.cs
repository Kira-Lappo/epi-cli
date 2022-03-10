using System.CommandLine;

namespace OptimizelyApiClient.Cli;

public partial class CommandFactory
{
	private Command CreateJobCommandArea()
	{
		var command = new Command("job")
		{
			CreateFollowCommand(),
		};

		return command;
	}

	private Command CreateFollowCommand()
	{
		var command = new Command("follow", "Track job status");
		command.AddAlias("f");

		var jobIdArgument = new Argument<Guid>("Job ID in in form of UUID.");
		command.AddArgument(jobIdArgument);

		command.SetAsyncHandler(
			(Guid jobId) => _handler.FollowJobAsync(jobId),
			jobIdArgument);

		return command;
	}
}

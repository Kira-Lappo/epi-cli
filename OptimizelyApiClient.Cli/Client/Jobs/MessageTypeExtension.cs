namespace OptimizelyApiClient.Cli.Client.Jobs;

public static class MessageTypeExtension
{
	public static bool IsFinished(this MessageType type)
	{
		return type == MessageType.Success;
	}
}

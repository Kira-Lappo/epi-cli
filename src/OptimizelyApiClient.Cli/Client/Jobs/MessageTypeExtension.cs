namespace OptimizelyApiClient.Cli.Client.Jobs;

public static class MessageTypeExtension
{
	public static bool IsFinished(this JobMessage message)
	{
		return message.MessageType is MessageType.Success
			or MessageType.Error;
	}

	public static bool IsSuccessful(this JobMessage message)
	{
		return message.MessageType is MessageType.Success;
	}
}

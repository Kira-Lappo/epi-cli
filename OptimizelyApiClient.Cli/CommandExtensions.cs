using System.CommandLine;
using System.CommandLine.Binding;

namespace OptimizelyApiClient.Cli;

public static class CommandExtensions
{
	public static void SetAsyncHandler<T1>(
		this Command command,
		Func<T1, Task> handle,
		params IValueDescriptor[] symbols)
	{
		command.SetHandler(
			(T1 param1) =>
			{
				handle?
					.Invoke(param1)
					.ConfigureAwait(false)
					.GetAwaiter()
					.GetResult();
			},
			symbols);
	}

	public static void SetAsyncHandler<T1, T2>(
		this Command command,
		Func<T1, T2, Task> handle,
		params IValueDescriptor[] symbols)
	{
		command.SetHandler(
			(T1 p1, T2 p2) =>
			{
				handle?
					.Invoke(p1, p2)
					.ConfigureAwait(false)
					.GetAwaiter()
					.GetResult();
			},
			symbols);
	}
}

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

	public static void SetAsyncHandler<T1, T2, T3>(
		this Command command,
		Func<T1, T2, T3, Task> handle,
		params IValueDescriptor[] symbols)
	{
		command.SetHandler(
			(T1 p1, T2 p2, T3 p3) =>
			{
				handle?
					.Invoke(p1, p2, p3)
					.ConfigureAwait(false)
					.GetAwaiter()
					.GetResult();
			},
			symbols);
	}

	public static void SetAsyncHandler<T1, T2, T3, T4>(
		this Command command,
		Func<T1, T2, T3, T4, Task> handle,
		params IValueDescriptor[] symbols)
	{
		command.SetHandler(
			(T1 p1, T2 p2, T3 p3, T4 p4) =>
			{
				handle?
					.Invoke(p1, p2, p3, p4)
					.ConfigureAwait(false)
					.GetAwaiter()
					.GetResult();
			},
			symbols);
	}
}

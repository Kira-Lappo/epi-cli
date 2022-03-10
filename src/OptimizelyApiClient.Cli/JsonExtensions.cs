using System.Text.Json;
using System.Text.Json.Nodes;

namespace OptimizelyApiClient.Cli;

public static class JsonExtensions
{
	public static T ConvertFromJson<T>(this string json, T defaultValue = default)
	{
		return JsonSerializer.Deserialize<T>(json) ?? defaultValue;
	}

	public static T ConvertFromJsonNode<T>(this string json, string path, T defaultValue = default)
	{
		var root = JsonNode.Parse(json);
		if (root == default)
		{
			return defaultValue;
		}

		var node = root[path];
		if (node == default)
		{
			return defaultValue;
		}

		return node.GetValue<T>() ?? defaultValue;
	}
}

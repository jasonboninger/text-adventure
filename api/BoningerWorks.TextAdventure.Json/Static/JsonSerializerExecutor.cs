using System.Text.Json;

namespace BoningerWorks.TextAdventure.Json.Static
{
	internal static class JsonSerializerExecutor
	{
		public static TValue Deserialize<TValue>(string json)
		{
			// Create options
			var options = JsonSerializerOptionsCreator.Create();
			// Create value
			var value = JsonSerializer.Deserialize<TValue>(json, options);
			// Return value
			return value;
		}
	}
}

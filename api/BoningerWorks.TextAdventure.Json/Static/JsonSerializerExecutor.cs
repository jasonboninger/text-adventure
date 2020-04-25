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
		public static TValue Deserialize<TValue>(ref Utf8JsonReader reader)
		{
			// Create options
			var options = JsonSerializerOptionsCreator.Create();
			// Create value
			var value = JsonSerializer.Deserialize<TValue>(ref reader, options);
			// Return value
			return value;
		}

		public static string Serialize<TValue>(TValue value)
		{
			// Create options
			var options = JsonSerializerOptionsCreator.Create();
			// Create JSON
			var json = JsonSerializer.Serialize(value, options);
			// Return JSON
			return json;
		}
		public static void Serialize<TValue>(Utf8JsonWriter writer, TValue value)
		{
			// Create options
			var options = JsonSerializerOptionsCreator.Create();
			// Write JSON
			JsonSerializer.Serialize(writer, value, options);
		}
	}
}

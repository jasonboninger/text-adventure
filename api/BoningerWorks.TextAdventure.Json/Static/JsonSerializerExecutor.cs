using BoningerWorks.TextAdventure.Json.Inputs;
using System.Text.Json;

namespace BoningerWorks.TextAdventure.Json.Static
{
	public static class JsonSerializerExecutor
	{
		public static Game DeserializeGame(string json)
		{
			// Return game
			return _Deserialize<Game>(json);
		}
		public static Game DeserializeGame(ref Utf8JsonReader reader)
		{
			// Return game
			return _Deserialize<Game>(ref reader);
		}

		public static string SerializeGame(Game game)
		{
			// Return JSON
			return _Serialize(game);
		}
		public static void SerializeGame(Utf8JsonWriter writer, Game game)
		{
			// Write JSON
			_Serialize(writer, game);
		}

		private static TValue _Deserialize<TValue>(string json)
		{
			// Create options
			var options = JsonSerializerOptionsCreator.Create();
			// Create value
			var value = JsonSerializer.Deserialize<TValue>(json, options);
			// Return value
			return value;
		}
		private static TValue _Deserialize<TValue>(ref Utf8JsonReader reader)
		{
			// Create options
			var options = JsonSerializerOptionsCreator.Create();
			// Create value
			var value = JsonSerializer.Deserialize<TValue>(ref reader, options);
			// Return value
			return value;
		}

		private static string _Serialize<TValue>(TValue value)
		{
			// Create options
			var options = JsonSerializerOptionsCreator.Create();
			// Create JSON
			var json = JsonSerializer.Serialize(value, options);
			// Return JSON
			return json;
		}
		private static void _Serialize<TValue>(Utf8JsonWriter writer, TValue value)
		{
			// Create options
			var options = JsonSerializerOptionsCreator.Create();
			// Write JSON
			JsonSerializer.Serialize(writer, value, options);
		}
	}
}

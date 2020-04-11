using BoningerWorks.TextAdventure.Engine.Json.Converters.Factories;
using System.Text.Json;

namespace BoningerWorks.TextAdventure.Engine.Static
{
	public static class JsonConfigurations
	{
		public static JsonSerializerOptions CreateOptions()
		{
			// Create JSON serializer options
			var jsonSerializerOptions = new JsonSerializerOptions();
			// Configure JSON serializer options
			ConfigureOptions(jsonSerializerOptions);
			// Return JSON serializer options
			return jsonSerializerOptions;
		}

		public static void ConfigureOptions(JsonSerializerOptions jsonSerializerOptions)
		{
			// Set not property name case insensitive
			jsonSerializerOptions.PropertyNameCaseInsensitive = false;
			// Add one or many list converter factory
			jsonSerializerOptions.Converters.Add(new OneOrManyListJsonConverterFactory());
		}
	}
}

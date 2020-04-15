using BoningerWorks.TextAdventure.Engine.Json.Converters;
using BoningerWorks.TextAdventure.Engine.Json.Converters.Factories;
using System.Text.Json;

namespace BoningerWorks.TextAdventure.Engine.Static
{
	public static class JsonConfigurations
	{
		private class PassThroughJsonNamingPolicy : JsonNamingPolicy
		{
			public override string ConvertName(string name)
			{
				// Return name
				return name;
			}
		}

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
			// Set property naming policy
			jsonSerializerOptions.PropertyNamingPolicy = new PassThroughJsonNamingPolicy();
			// Set dictionary key naming policy
			jsonSerializerOptions.DictionaryKeyPolicy = new PassThroughJsonNamingPolicy();
			// Set not ignore null values
			jsonSerializerOptions.IgnoreNullValues = false;
			// Set not ignore read only properties
			jsonSerializerOptions.IgnoreReadOnlyProperties = false;
			// Set not allow trailing commas
			jsonSerializerOptions.AllowTrailingCommas = false;
			// Set not write indented
			jsonSerializerOptions.WriteIndented = false;
			// Set max depth
			jsonSerializerOptions.MaxDepth = 128;
			// Disallow comments
			jsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Disallow;
			// Add converter factories
			jsonSerializerOptions.Converters.Add(new OneOrManyListJsonConverterFactory());
			// Add converters
			jsonSerializerOptions.Converters.Add(new LineBlueprintJsonConverter());
			jsonSerializerOptions.Converters.Add(new MessageBlueprintJsonConverter());
			jsonSerializerOptions.Converters.Add(new SymbolJsonConverter());
			jsonSerializerOptions.Converters.Add(new SymbolToEntityStateMappingsJsonConverter());
		}
	}
}

using BoningerWorks.TextAdventure.Engine.Blueprints.Lines;
using BoningerWorks.TextAdventure.Engine.Blueprints.Messages;
using BoningerWorks.TextAdventure.Engine.Json.Converters;
using BoningerWorks.TextAdventure.Engine.Json.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Json.Static
{
	public static class JsonConfigurations
	{
		private class PassThroughJsonNamingPolicy : JsonNamingPolicy
		{
			public static PassThroughJsonNamingPolicy Instance { get; } = new PassThroughJsonNamingPolicy();

			public override string ConvertName(string name)
			{
				// Return name
				return name;
			}

			private PassThroughJsonNamingPolicy() { }
		}

		public static JsonSerializerOptions CreateOptions()
		{
			// Create options
			var options = new JsonSerializerOptions();
			// Configure options
			ConfigureOptions(options);
			// Return options
			return options;
		}

		public static void ConfigureOptions(JsonSerializerOptions options)
		{
			// Set not property name case insensitive
			options.PropertyNameCaseInsensitive = false;
			// Set property naming policy
			options.PropertyNamingPolicy = PassThroughJsonNamingPolicy.Instance;
			// Set dictionary key naming policy
			options.DictionaryKeyPolicy = PassThroughJsonNamingPolicy.Instance;
			// Set not ignore null values
			options.IgnoreNullValues = false;
			// Set not ignore read only properties
			options.IgnoreReadOnlyProperties = false;
			// Set not allow trailing commas
			options.AllowTrailingCommas = false;
			// Set not write indented
			options.WriteIndented = false;
			// Set max depth
			options.MaxDepth = 128;
			// Disallow comments
			options.ReadCommentHandling = JsonCommentHandling.Disallow;
			// Add converters
			options.Converters.AddRange
				(
					new JsonStringEnumConverter(PassThroughJsonNamingPolicy.Instance, allowIntegerValues: false),
					new OneOrManyListJsonConverterFactory(),
					new SymbolDictionaryJsonConverterFactory(),
					new FlexibleObjectJsonConverter<LineBlueprint>(LineBlueprint.CreateFromString),
					new FlexibleObjectJsonConverter<MessageBlueprint>(MessageBlueprint.CreateFromString)
				);
		}
	}
}

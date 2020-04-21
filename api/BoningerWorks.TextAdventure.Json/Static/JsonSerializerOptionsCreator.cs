using BoningerWorks.TextAdventure.Json.Converters;
using BoningerWorks.TextAdventure.Json.Models;
using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Static
{
	public static class JsonSerializerOptionsCreator
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

		private readonly static ImmutableArray<JsonConverter> _converters = ImmutableArray.Create<JsonConverter>
			(
				new JsonStringEnumConverter(PassThroughJsonNamingPolicy.Instance, allowIntegerValues: false),
				new OneOrManyListJsonConverterFactory(),
				new SymbolDictionaryJsonConverterFactory(),
				new FlexibleObjectJsonConverter<Line>(Line.CreateFromString),
				new FlexibleObjectJsonConverter<Message>(Message.CreateFromString)
			);

		public static JsonSerializerOptions Create()
		{
			// Create options
			var options = new JsonSerializerOptions
			{
				// Set not property name case insensitive
				PropertyNameCaseInsensitive = false,
				// Set property naming policy
				PropertyNamingPolicy = PassThroughJsonNamingPolicy.Instance,
				// Set dictionary key naming policy
				DictionaryKeyPolicy = PassThroughJsonNamingPolicy.Instance,
				// Set not ignore null values
				IgnoreNullValues = false,
				// Set not ignore read only properties
				IgnoreReadOnlyProperties = false,
				// Set not allow trailing commas
				AllowTrailingCommas = false,
				// Set not write indented
				WriteIndented = false,
				// Set max depth
				MaxDepth = 128,
				// Disallow comments
				ReadCommentHandling = JsonCommentHandling.Disallow
			};
			// Add converters
			for (int i = 0; i < _converters.Length; i++)
			{
				// Add converter
				options.Converters.Add(_converters[i]);
			}
			// Return options
			return options;
		}
	}
}

using BoningerWorks.TextAdventure.Json.Converters;
using BoningerWorks.TextAdventure.Json.Inputs;
using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Static
{
	internal static class JsonSerializerOptionsCreator
	{
		private class PassThroughJsonNamingPolicy : JsonNamingPolicy
		{
			public static PassThroughJsonNamingPolicy Instance { get; } = new PassThroughJsonNamingPolicy();

			public override string ConvertName(string name) => name;

			private PassThroughJsonNamingPolicy() { }
		}

		private readonly static ImmutableList<JsonConverter> _converters = ImmutableList.Create<JsonConverter>
			(
				new JsonStringEnumConverter(PassThroughJsonNamingPolicy.Instance, allowIntegerValues: false),
				new OneOrManyListJsonConverterFactory(),
				new FlexibleObjectJsonConverter<Message>(createFromString: Message.CreateFromString),
				new FlexibleObjectJsonConverter<Line>(createFromString: Line.CreateFromString),
				new FlexibleObjectJsonConverter<Text>(createFromString: Text.CreateFromString),
				new SymbolJsonConverter(),
				new SymbolDictionaryJsonConverterFactory()
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
			// Get converters
			var converters = options.Converters;
			// Add converters
			_converters.ForEach(converters.Add);
			// Return options
			return options;
		}
	}
}

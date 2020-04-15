using BoningerWorks.TextAdventure.Engine.States;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Json.Converters
{
	public class SymbolToEntityStateMappingsJsonConverter : JsonConverter<Dictionary<Symbol, EntityState>>
	{
		public override Dictionary<Symbol, EntityState> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			// Create string to entity state mappings
			var stringToEntityStateMappings = JsonSerializer.Deserialize<Dictionary<string, EntityState>>(ref reader, options);
			// Return symbol to entity state mappings
			return stringToEntityStateMappings.ToDictionary(kv => new Symbol(kv.Key), kv => kv.Value);
		}

		public override void Write(Utf8JsonWriter writer, Dictionary<Symbol, EntityState> value, JsonSerializerOptions options)
		{
			// Create string to entity state mappings
			var stringToEntityStateMappings = value.ToDictionary(kv => kv.Key.ToString(), kv => kv.Value);
			// Write symbol to entity state mappings
			JsonSerializer.Serialize(writer, stringToEntityStateMappings, options);
		}
	}
}

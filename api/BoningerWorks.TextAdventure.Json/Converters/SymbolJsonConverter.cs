using BoningerWorks.TextAdventure.Core.Utilities;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Converters
{
	public class SymbolJsonConverter : JsonConverter<Symbol>
	{
		public override Symbol Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			// Return symbol
			return new Symbol(reader.GetString());
		}

		public override void Write(Utf8JsonWriter writer, Symbol value, JsonSerializerOptions options)
		{
			// Write symbol
			writer.WriteStringValue(value.ToString());
		}
	}
}

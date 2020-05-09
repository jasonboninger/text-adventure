using BoningerWorks.TextAdventure.Core.Utilities;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Converters
{
	public class IdJsonConverter : JsonConverter<Id>
	{
		public override Id Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			// Return ID
			return new Id(reader.GetString());
		}

		public override void Write(Utf8JsonWriter writer, Id value, JsonSerializerOptions options)
		{
			// Write ID
			writer.WriteStringValue(value.ToString());
		}
	}
}

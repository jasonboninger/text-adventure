using BoningerWorks.TextAdventure.Engine.Blueprints.Lines;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Json.Converters
{
	public class LineBlueprintJsonConverter : JsonConverter<LineBlueprint>
	{
		public override LineBlueprint Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			// Create JSON document
			using var jsonDocument = JsonDocument.ParseValue(ref reader);
			// Get JSON element
			var jsonElement = jsonDocument.RootElement;
			// Check value kind
			switch (jsonElement.ValueKind)
			{
				case JsonValueKind.String:
					// Return text line blueprint
					return new LineBlueprint
					{
						Inlined = new LineInlinedBlueprint
						{
							Text = jsonElement.GetString()
						}
					};
				case JsonValueKind.Object:
					// Check if input exists
					if (jsonElement.TryGetProperty("input", out var input))
					{
						// Return input line blueprint
						return new LineBlueprint
						{
							Input = input.GetString()
						};
					}
					// Check if special exists
					if (jsonElement.TryGetProperty("special", out var special))
					{
						// Return special line blueprint
						return new LineBlueprint
						{
							Special = special.GetString()
						};
					}
					// Check if text exists
					if (jsonElement.TryGetProperty("text", out _))
					{
						// Return inlined line blueprint
						return new LineBlueprint
						{
							Inlined = JsonSerializer.Deserialize<LineInlinedBlueprint>(jsonElement.GetRawText(), options)
						};
					}
					break;
			}
			// Throw error
			throw new InvalidOperationException("JSON for line blueprint could not be parsed.");
		}

		public override void Write(Utf8JsonWriter writer, LineBlueprint value, JsonSerializerOptions options)
		{
			// Throw error
			throw new NotImplementedException();
		}
	}
}

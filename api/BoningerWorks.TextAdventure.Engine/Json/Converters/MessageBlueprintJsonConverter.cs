using BoningerWorks.TextAdventure.Engine.Blueprints.Messages;
using BoningerWorks.TextAdventure.Engine.Blueprints.Messages.Lines;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Json.Converters
{
	public class MessageBlueprintJsonConverter : JsonConverter<MessageBlueprint>
	{
		public override MessageBlueprint Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			// Create JSON document
			using var jsonDocument = JsonDocument.ParseValue(ref reader);
			// Get JSON element
			var jsonElement = jsonDocument.RootElement;
			// Check value kind
			switch (jsonElement.ValueKind)
			{
				case JsonValueKind.String:
					// Return text message blueprint
					return new MessageBlueprint
					{
						Inlined = new MessageInlinedBlueprint
						{
							Lines = JsonSerializer.Deserialize<OneOrManyList<LineBlueprint>>(jsonElement.ToString())
						}
					};
				case JsonValueKind.Object:
					// Check if input exists
					if (jsonElement.TryGetProperty("input", out var input))
					{
						// Return input message blueprint
						return new MessageBlueprint
						{
							Input = input.GetString()
						};
					}
					// Check if template exists
					if (jsonElement.TryGetProperty("template", out _))
					{
						// Return templated message blueprint
						return new MessageBlueprint
						{
							Templated = JsonSerializer.Deserialize<MessageTemplatedBlueprint>(jsonElement.ToString(), options)
						};
					}
					// Check if lines exist
					if (jsonElement.TryGetProperty("lines", out _))
					{
						// Return inlined message blueprint
						return new MessageBlueprint
						{
							Inlined = JsonSerializer.Deserialize<MessageInlinedBlueprint>(jsonElement.ToString(), options)
						};
					}
					break;
			}
			// Throw error
			throw new InvalidOperationException("JSON for message blueprint could not be parsed.");
		}

		public override void Write(Utf8JsonWriter writer, MessageBlueprint value, JsonSerializerOptions options)
		{
			// Throw error
			throw new NotImplementedException();
		}
	}
}

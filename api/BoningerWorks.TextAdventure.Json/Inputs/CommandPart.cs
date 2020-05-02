using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class CommandPart
	{
		public static CommandPart ReadFromArray(ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			// Return command part
			return new CommandPart { Words = JsonSerializer.Deserialize<List<string?>?>(ref reader, options) };
		}

		[JsonPropertyName("words")] public List<string?>? Words { get; set; }
		[JsonPropertyName("area")] public string? Area { get; set; }
		[JsonPropertyName("item")] public string? Item { get; set; }
	}
}

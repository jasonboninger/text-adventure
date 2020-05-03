using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Change
	{
		[JsonPropertyName("target")] public string? Target { get; set; }
		[JsonPropertyName("standard")] public string? Standard { get; set; }
		[JsonPropertyName("custom")] public string? Custom { get; set; }
		[JsonPropertyName("value")] public string? Value { get; set; }
	}
}

using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Change
	{
		[JsonPropertyName("target")] public string? Target { get; set; }
		[JsonPropertyName("datum")] public string? Datum { get; set; }
		[JsonPropertyName("value")] public string? Value { get; set; }
	}
}

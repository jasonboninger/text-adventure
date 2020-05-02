using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class ReactionInput
	{
		[JsonPropertyName("input")] public string? Input { get; set; }
		[JsonPropertyName("value")] public string? Value { get; set; }
	}
}

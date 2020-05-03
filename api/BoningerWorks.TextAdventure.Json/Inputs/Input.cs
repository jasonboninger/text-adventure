using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Input
	{
		[JsonPropertyName("key")] public string? Key { get; set; }
		[JsonPropertyName("value")] public string? Value { get; set; }
	}
}

using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class ConditionItem
	{
		[JsonPropertyName("item")] public string? Item { get; set; }
		[JsonPropertyName("condition")] public Condition? Condition { get; set; }
	}
}

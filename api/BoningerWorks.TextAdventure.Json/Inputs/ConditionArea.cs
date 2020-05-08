using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class ConditionArea
	{
		[JsonPropertyName("area")] public string? Area { get; set; }
		[JsonPropertyName("condition")] public Condition? Condition { get; set; }
	}
}

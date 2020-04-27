using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Condition
	{
		[JsonPropertyName("left")] public string? Left { get; set; }
		[JsonPropertyName("comparison")] public string? Comparison { get; set; }
		[JsonPropertyName("right")] public string? Right { get; set; }
		[JsonPropertyName("operator")] public string? Operator { get; set; }
		[JsonPropertyName("conditions")] public OneOrManyList<Condition?>? Conditions { get; set; }
	}
}

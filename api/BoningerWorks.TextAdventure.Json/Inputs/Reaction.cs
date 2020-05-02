using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Reaction
	{
		[JsonPropertyName("command")] public string? CommandSymbol { get; set; }
		[JsonPropertyName("inputs")] public List<ReactionInput?>? ReactionInputs { get; set; }
		[JsonPropertyName("actions")] public OneOrManyList<Action?>? Actions { get; set; }
	}
}

using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Reaction
	{
		[JsonPropertyName("command")] public string? CommandId { get; set; }
		[JsonPropertyName("inputs")] public OneOrManyList<Input?>? Inputs { get; set; }
		[JsonPropertyName("actions")] public OneOrManyList<Action?>? Actions { get; set; }
	}
}

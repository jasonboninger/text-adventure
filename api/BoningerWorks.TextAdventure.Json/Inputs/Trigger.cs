using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Trigger
	{
		[JsonPropertyName("command")] public string? CommandSymbol { get; set; }
		[JsonPropertyName("inputs")] public OneOrManyList<Input?>? Inputs { get; set; }
	}
}

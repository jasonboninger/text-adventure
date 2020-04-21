using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Models
{
	public class Reaction
	{
		[JsonPropertyName("command")] public string CommandSymbol { get; set; }
		[JsonPropertyName("items")] public Dictionary<string, string> CommandItemSymbolToItemSymbolMappings { get; set; }
		[JsonPropertyName("actions")] public OneOrManyList<Action> Actions { get; set; }
	}
}

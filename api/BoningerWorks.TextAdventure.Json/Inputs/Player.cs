using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Player
	{
		[JsonPropertyName("names")] public OneOrManyList<string?>? Names { get; set; }
		[JsonPropertyName("area")] public string? AreaSymbol { get; set; }
		[JsonPropertyName("items")] public Dictionary<string, Item?>? ItemSymbolToItemMappings { get; set; }
		[JsonPropertyName("reactions")] public OneOrManyList<Reaction?>? Reactions { get; set; }
	}
}

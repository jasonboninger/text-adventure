using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Player
	{
		[JsonPropertyName("area")] public string? AreaSymbol { get; set; }
		[JsonPropertyName("items")] public Dictionary<string, Item?>? ItemSymbolToItemMappings { get; set; }
	}
}

using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Item
	{
		[JsonPropertyName("names")] public OneOrManyList<string> Names { get; set; }
		[JsonPropertyName("active")] public bool? Active { get; set; }
		[JsonPropertyName("reactions")] public OneOrManyList<Reaction> Reactions { get; set; }
		[JsonPropertyName("items")] public Dictionary<string, Item> ItemSymbolToItemMappings { get; set; }
	}
}

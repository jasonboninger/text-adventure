using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Models
{
	public class Area
	{
		[JsonPropertyName("items")] public Dictionary<string, Item> ItemSymbolToItemMappings { get; set; }
		[JsonPropertyName("reactions")] public OneOrManyList<Reaction> Reactions { get; set; }
	}
}

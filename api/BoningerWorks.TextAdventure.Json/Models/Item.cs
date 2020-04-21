using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Models
{
	public class Item
	{
		[JsonPropertyName("names")] public OneOrManyList<string> Names { get; set; }
		[JsonPropertyName("active")] public bool? Active { get; set; }
		[JsonPropertyName("reactions")] public Dictionary<string, Reaction> Reactions { get; set; }
	}
}

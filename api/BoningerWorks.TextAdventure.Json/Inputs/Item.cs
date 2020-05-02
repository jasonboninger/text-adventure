using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Item
	{
		[JsonPropertyName("id")] public string? Id { get; set; }
		[JsonPropertyName("names")] public List<string?>? Names { get; set; }
		[JsonPropertyName("active")] public bool? Active { get; set; }
		[JsonPropertyName("reactions")] public OneOrManyList<Reaction?>? Reactions { get; set; }
		[JsonPropertyName("items")] public List<Item?>? Items { get; set; }
	}
}

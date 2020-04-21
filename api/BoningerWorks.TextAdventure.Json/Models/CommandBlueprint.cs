using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Models
{
	public class CommandBlueprint
	{
		[JsonPropertyName("parts")] public OneOrManyList<string> Parts { get; set; }
		[JsonPropertyName("words")] public Dictionary<string, OneOrManyList<string>> Words { get; set; }
		[JsonPropertyName("items")] public OneOrManyList<string> Items { get; set; }
	}
}

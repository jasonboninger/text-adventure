using BoningerWorks.TextAdventure.Engine.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Instructions.Items
{
	public class ItemBlueprint
	{
		[JsonPropertyName("names")] public OneOrManyList<string> Names { get; set; }
		[JsonPropertyName("active")] public bool? Active { get; set; }
		[JsonPropertyName("commands")] public Dictionary<string, CommandBlueprint> Commands { get; set; }
	}
}

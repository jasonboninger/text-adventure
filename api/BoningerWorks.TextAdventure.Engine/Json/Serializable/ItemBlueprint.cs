using BoningerWorks.TextAdventure.Engine.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Json.Serializable
{
	public class ItemBlueprint
	{
		[JsonPropertyName("names")] public OneOrManyList<string> Names { get; set; }
		[JsonPropertyName("active")] public bool? Active { get; set; }
		[JsonPropertyName("commands")] public Dictionary<string, ReactionBlueprint> Commands { get; set; }
	}
}

using BoningerWorks.TextAdventure.Engine.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Json.Serializable
{
	public class ReactionBlueprint
	{
		[JsonPropertyName("items")] public Dictionary<string, string> Items { get; set; }
		[JsonPropertyName("actions")] public OneOrManyList<ActionBlueprint> Actions { get; set; }
	}
}

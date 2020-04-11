using BoningerWorks.TextAdventure.Engine.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Instructions
{
	public class CommandBlueprint
	{
		[JsonPropertyName("items")] public Dictionary<string, string> Items { get; set; }
		[JsonPropertyName("actions")] public OneOrManyList<ActionBlueprint> Actions { get; set; }
	}
}

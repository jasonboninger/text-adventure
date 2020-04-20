using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Json.Serializable
{
	public class PlayerBlueprint
	{
		[JsonPropertyName("area")] public string Area { get; set; }
		[JsonPropertyName("items")] public Dictionary<string, ItemBlueprint> Items { get; set; }
	}
}

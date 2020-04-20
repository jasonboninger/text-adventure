using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Json.Serializable
{
	public class GameBlueprint
	{
		[JsonPropertyName("commands")] public Dictionary<string, CommandBlueprint> Commands { get; set; }
		[JsonPropertyName("player")] public PlayerBlueprint Player { get; set; }
	}
}

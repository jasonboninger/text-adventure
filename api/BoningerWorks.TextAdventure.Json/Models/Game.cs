using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Models
{
	public class Game
	{
		[JsonPropertyName("commands")] public Dictionary<string, Command> CommandSymbolToCommandMappings { get; set; }
		[JsonPropertyName("player")] public Player Player { get; set; }
	}
}

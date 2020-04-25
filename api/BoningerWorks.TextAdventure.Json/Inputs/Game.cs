using BoningerWorks.TextAdventure.Json.Static;
using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Game
	{
		public static Game Deserialize(string json) => JsonSerializerExecutor.Deserialize<Game>(json);

		[JsonPropertyName("commands")] public Dictionary<string, Command?>? CommandSymbolToCommandMappings { get; set; }
		[JsonPropertyName("player")] public Player? Player { get; set; }
		[JsonPropertyName("areas")] public Dictionary<string, Area?>? AreaSymbolToAreaMappings { get; set; }
		[JsonPropertyName("start")] public OneOrManyList<Action?>? ActionsStart { get; set; }
		[JsonPropertyName("end")] public OneOrManyList<Action?>? ActionsEnd { get; set; }
	}
}

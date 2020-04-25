using BoningerWorks.TextAdventure.Json.Static;
using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Game
	{
		public static Game Deserialize(string json) => JsonSerializerExecutor.Deserialize<Game>(json);
		public static Game Deserialize(ref Utf8JsonReader reader) => JsonSerializerExecutor.Deserialize<Game>(ref reader);

		public static string Serialize(Game game) => JsonSerializerExecutor.Serialize(game);
		public static void Serialize(Utf8JsonWriter writer, Game game) => JsonSerializerExecutor.Serialize(writer, game);

		[JsonPropertyName("commands")] public Dictionary<string, Command?>? CommandSymbolToCommandMappings { get; set; }
		[JsonPropertyName("player")] public Player? Player { get; set; }
		[JsonPropertyName("areas")] public Dictionary<string, Area?>? AreaSymbolToAreaMappings { get; set; }
		[JsonPropertyName("start")] public OneOrManyList<Action?>? ActionsStart { get; set; }
		[JsonPropertyName("end")] public OneOrManyList<Action?>? ActionsEnd { get; set; }
	}
}

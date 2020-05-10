using BoningerWorks.TextAdventure.Json.Static;
using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Game
	{
		public static Game? Deserialize(string json) => JsonSerializerExecutor.Deserialize<Game?>(json);

		[JsonPropertyName("commands")] public List<Command?>? Commands { get; set; }
		[JsonPropertyName("player")] public Player? Player { get; set; }
		[JsonPropertyName("areas")] public List<Area?>? Areas { get; set; }
		[JsonPropertyName("start")] public OneOrManyList<Action?>? ActionsStart { get; set; }
		[JsonPropertyName("end")] public OneOrManyList<Action?>? ActionsEnd { get; set; }
		[JsonPropertyName("prompt")] public OneOrManyList<Action?>? ActionsPrompt { get; set; }
		[JsonPropertyName("fail")] public OneOrManyList<Action?>? ActionsFail { get; set; }
		[JsonPropertyName("areaAmbiguous")] public OneOrManyList<Action?>? ActionsAreaAmbiguous { get; set; }
		[JsonPropertyName("itemAmbiguous")] public OneOrManyList<Action?>? ActionsItemAmbiguous { get; set; }
		[JsonPropertyName("areaInContext")] public ConditionArea? ConditionArea { get; set; }
		[JsonPropertyName("itemInContext")] public ConditionItem? ConditionItem { get; set; }
		[JsonPropertyName("options")] public Options? Options { get; set; }
	}
}

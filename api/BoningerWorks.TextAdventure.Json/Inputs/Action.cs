using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Action 
	{
		[JsonPropertyName("if")] public If<Action> If { get; set; }
		[JsonPropertyName("messages")] public OneOrManyList<FlexibleObject<Message>> Messages { get; set; }
		[JsonPropertyName("changes")] public Dictionary<string, string> ChangePathToNewValueMappings { get; set; }
		[JsonPropertyName("triggers")] public OneOrManyList<Trigger> Triggers { get; set; }
	}
}

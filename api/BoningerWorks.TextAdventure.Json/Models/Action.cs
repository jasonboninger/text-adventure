using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Models
{
	public class Action 
	{
		[JsonPropertyName("if")] public If<Action> If { get; set; }
		[JsonPropertyName("messages")] public OneOrManyList<Message> Messages { get; set; }
	}
}

using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Models
{
	public class Action 
	{
		[JsonPropertyName("messages")] public OneOrManyList<Message> Messages { get; set; }
	}
}

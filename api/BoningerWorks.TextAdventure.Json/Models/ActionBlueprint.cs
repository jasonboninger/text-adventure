using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Models
{
	public class ActionBlueprint 
	{
		[JsonPropertyName("messages")] public OneOrManyList<MessageBlueprint> Messages { get; set; }
	}
}

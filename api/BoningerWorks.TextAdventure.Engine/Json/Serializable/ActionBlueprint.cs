using BoningerWorks.TextAdventure.Engine.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Json.Serializable
{
	public class ActionBlueprint 
	{
		[JsonPropertyName("messages")] public OneOrManyList<MessageBlueprint> Messages { get; set; }
	}
}

using BoningerWorks.TextAdventure.Engine.Blueprints.Messages;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Blueprints
{
	public class ActionBlueprint 
	{
		[JsonPropertyName("messages")] public OneOrManyList<MessageBlueprint> Messages { get; set; }
	}
}

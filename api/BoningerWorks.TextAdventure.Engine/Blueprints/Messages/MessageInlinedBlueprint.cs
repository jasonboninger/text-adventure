using BoningerWorks.TextAdventure.Engine.Blueprints.Lines;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Blueprints.Messages
{
	public class MessageInlinedBlueprint
	{
		[JsonPropertyName("lines")] public OneOrManyList<LineBlueprint> Lines { get; set; }
	}
}

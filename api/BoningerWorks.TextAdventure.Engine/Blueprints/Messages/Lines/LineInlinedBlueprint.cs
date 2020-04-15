using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Blueprints.Messages.Lines
{
	public class LineInlinedBlueprint
	{
		[JsonPropertyName("text")] public string Text { get; set; }
	}
}

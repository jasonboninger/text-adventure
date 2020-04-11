using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Instructions
{
	public class GameBlueprint
	{
		[JsonPropertyName("templates")] public TemplatesBlueprint Templates { get; set; }
		[JsonPropertyName("player")] public PlayerBlueprint Player { get; set; }
	}
}

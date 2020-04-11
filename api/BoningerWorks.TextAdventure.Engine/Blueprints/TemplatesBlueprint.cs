using BoningerWorks.TextAdventure.Engine.Blueprints.Templates;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Blueprints
{
	public class TemplatesBlueprint
	{
		[JsonPropertyName("commands")] public Dictionary<string, CommandTemplate> Commands { get; set; }
	}
}

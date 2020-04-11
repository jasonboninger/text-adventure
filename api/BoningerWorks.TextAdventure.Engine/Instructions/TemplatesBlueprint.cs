using BoningerWorks.TextAdventure.Engine.Instructions.Templates;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Instructions
{
	public class TemplatesBlueprint
	{
		[JsonPropertyName("commands")] public Dictionary<string, CommandTemplate> Commands { get; set; }
	}
}

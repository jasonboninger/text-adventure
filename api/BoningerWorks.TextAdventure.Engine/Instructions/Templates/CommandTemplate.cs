using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Instructions.Templates
{
	public class CommandTemplate
	{
		[JsonPropertyName("template")] public List<string> Parts { get; set; }
		[JsonPropertyName("words")] public Dictionary<string, List<string>> Words { get; set; }
		[JsonPropertyName("items")] public List<string> Items { get; set; }
	}
}

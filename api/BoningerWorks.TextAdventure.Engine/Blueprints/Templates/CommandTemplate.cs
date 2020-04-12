using BoningerWorks.TextAdventure.Engine.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Blueprints.Templates
{
	public class CommandTemplate
	{
		[JsonPropertyName("template")] public OneOrManyList<string> Parts { get; set; }
		[JsonPropertyName("words")] public Dictionary<string, OneOrManyList<string>> Words { get; set; }
		[JsonPropertyName("items")] public OneOrManyList<string> Items { get; set; }
	}
}

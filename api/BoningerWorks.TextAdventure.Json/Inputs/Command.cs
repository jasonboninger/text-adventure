using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Command
	{
		[JsonPropertyName("parts")] public OneOrManyList<string>? PartSymbols { get; set; }
		[JsonPropertyName("words")] public Dictionary<string, OneOrManyList<string>>? WordSymbolToWordNamesMappings { get; set; }
		[JsonPropertyName("items")] public OneOrManyList<string>? CommandItemSymbols { get; set; }
	}
}

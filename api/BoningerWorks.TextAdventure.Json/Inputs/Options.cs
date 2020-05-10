using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Options
	{
		[JsonPropertyName("ignoredCharacters")] public List<string?>? IgnoredCharacters { get; set; }
	}
}

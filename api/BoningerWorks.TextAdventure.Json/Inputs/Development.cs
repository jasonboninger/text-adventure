using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Development
	{
		[JsonPropertyName("testCommands")] public List<string?>? TestCommands { get; set; }
	}
}

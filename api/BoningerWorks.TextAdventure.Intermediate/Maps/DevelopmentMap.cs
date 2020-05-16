using BoningerWorks.TextAdventure.Json.Inputs;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class DevelopmentMap
	{
		public ImmutableArray<string> TestCommands { get; }

		public DevelopmentMap(Development? development)
		{
			// Set test commands
			TestCommands = development?.TestCommands?.Select(tc => tc ?? string.Empty).ToImmutableArray() ?? ImmutableArray<string>.Empty;
		}
	}
}

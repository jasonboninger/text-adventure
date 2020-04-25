using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class LineInlinedMap
	{
		public ImmutableArray<TextMap> TextMaps { get; }

		internal LineInlinedMap(OneOrManyList<SFlexibleObject<Text>>? texts)
		{
			// Check if texts does not exist
			if (texts == null)
			{
				// Throw error
				throw new ValidationError("Texts cannot be null.");
			}
			// Set text maps
			TextMaps = texts.Select(t => new TextMap(t)).ToImmutableArray();
		}
	}
}

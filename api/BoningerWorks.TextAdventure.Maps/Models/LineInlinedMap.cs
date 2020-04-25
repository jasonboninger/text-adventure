using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Json.Utilities;
using BoningerWorks.TextAdventure.Maps.Errors;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Maps.Models
{
	public class LineInlinedMap
	{
		public ImmutableArray<TextMap> TextMaps { get; }

		public LineInlinedMap(OneOrManyList<SFlexibleObject<Text>>? texts)
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

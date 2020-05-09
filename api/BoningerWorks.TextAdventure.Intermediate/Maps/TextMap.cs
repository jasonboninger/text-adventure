using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class TextMap
	{
		public ImmutableArray<IteratorMap<TextMap>>? IteratorMaps { get; }
		public IfMap<TextMap>? IfMap { get; }
		public TextInlinedMap? InlinedMap { get; }

		internal TextMap(Text? text)
		{
			// Check if text does not exist
			if (text == null)
			{
				// Throw error
				throw new ValidationError("Text cannot be null.");
			}
			// Create count
			var count = 0;
			// Check if iterators exists
			if (text.Iterators != null)
			{
				// Increase count
				count++;
				// Check if iterators is empty
				if (text.Iterators.Count == 0)
				{
					// Throw error
					throw new ValidationError("Iterators cannot be empty.");
				}
				// Set iterator maps
				IteratorMaps = text.Iterators.Select(i => IteratorMap<TextMap>.Create(i, t => new TextMap(t))).ToImmutableArray();
			}
			// Check if if exists
			if (text.If != null)
			{
				// Increase count
				count++;
				// Set if map
				IfMap = IfMap<TextMap>.Create(text.If, t => new TextMap(t));
			}
			// Check if value exists
			if (text.Value != null)
			{
				// Increase count
				count++;
				// Set inlined text map
				InlinedMap = new TextInlinedMap(text.Value);
			}
			// Check if count is not one
			if (count != 1)
			{
				// Throw error
				throw new ValidationError($"Text must have exactly one value, but instead has {count}.");
			}
		}
	}
}

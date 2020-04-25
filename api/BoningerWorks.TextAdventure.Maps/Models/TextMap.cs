using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Maps.Enums;
using BoningerWorks.TextAdventure.Maps.Errors;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Maps.Models
{
	public class TextMap
	{
		public ETextMapType Type { get; }
		public IfMap<TextMap>? IfMap { get; }
		public TextInlinedMap? InlinedMap { get; }

		public TextMap(Text? text)
		{
			// Check if text does not exist
			if (text == null)
			{
				// Throw error
				throw new ValidationError("Text cannot be null.");
			}
			// Create count
			var count = 0;
			// Check if if exists
			if (text.If != null)
			{
				// Increase count
				count++;
				// Set type
				Type = ETextMapType.If;
				// Set if map
				IfMap = new IfMap<TextMap>
					(
						new ConditionMap(text.If.Condition),
						text.If.ValuesTrue?.Select(t => new TextMap(t)).ToImmutableArray(),
						text.If.ValuesFalse?.Select(t => new TextMap(t)).ToImmutableArray()
					);
			}
			// Check if value exists
			if (text.Value != null)
			{
				// Increase count
				count++;
				// Set type
				Type = ETextMapType.Inlined;
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

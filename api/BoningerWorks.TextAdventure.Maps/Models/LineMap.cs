using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Maps.Enums;
using BoningerWorks.TextAdventure.Maps.Errors;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Maps.Models
{
	public class LineMap
	{
		public ELineMapType Type { get; }
		public IfMap<LineMap>? IfMap { get; }
		public LineSpecialMap? SpecialMap { get; }
		public LineInlinedMap? InlinedMap { get; }

		internal LineMap(Line? line)
		{
			// Check if line does not exist
			if (line == null)
			{
				// Throw error
				throw new ValidationError("Line cannot be null.");
			}
			// Create count
			var count = 0;
			// Check if if exists
			if (line.If != null)
			{
				// Increase count
				count++;
				// Set type
				Type = ELineMapType.If;
				// Set if map
				IfMap = new IfMap<LineMap>
					(
						new ConditionMap(line.If.Condition),
						line.If.ValuesTrue?.Select(l => new LineMap(l)).ToImmutableArray(),
						line.If.ValuesFalse?.Select(l => new LineMap(l)).ToImmutableArray()
					);
			}
			// Check if special exists
			if (line.Special != null)
			{
				// Increase count
				count++;
				// Set type
				Type = ELineMapType.Special;
				// Set special line map
				SpecialMap = new LineSpecialMap(line.Special);
			}
			// Check if texts exists
			if (line.Texts != null)
			{
				// Increase count
				count++;
				// Set type
				Type = ELineMapType.Inlined;
				// Set inlined line map
				InlinedMap = new LineInlinedMap(line.Texts);
			}
			// Check if count is not one
			if (count != 1)
			{
				// Throw error
				throw new ValidationError($"Line must have exactly one value, but instead has {count}.");
			}
		}
	}
}

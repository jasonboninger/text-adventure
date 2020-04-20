using BoningerWorks.TextAdventure.Engine.Blueprints.Lines;
using System;

namespace BoningerWorks.TextAdventure.Engine.Maps
{
	public class LineMap
	{
		public ELineMapType Type { get; }
		public LineInlinedMap Inlined { get; }
		public LineSpecialMap Special { get; }

		public LineMap(LineBlueprint lineBlueprint)
		{
			// Check if line bluprint does not exist
			if (lineBlueprint == null)
			{
				// Throw error
				throw new ArgumentException("Line blueprint cannot be null.", nameof(lineBlueprint));
			}
			// Create count
			var count = 0;
			// Check if inlined
			if (lineBlueprint.Inlined != null)
			{
				// Increase count
				count++;
				// Set type
				Type = ELineMapType.Inlined;
				// Set inlined
				Inlined = new LineInlinedMap(lineBlueprint.Inlined);
			}
			// Check if special
			if (lineBlueprint.Special != null)
			{
				// Increase count
				count++;
				// Set type
				Type = ELineMapType.Special;
				// Set special
				Special = new LineSpecialMap(lineBlueprint.Special);
			}
			// Check if count is not one
			if (count != 1)
			{
				// Throw error
				throw new ArgumentException($"Line blueprint must have exactly one value, but instead has {count}.", nameof(lineBlueprint));
			}
		}
	}
}

using BoningerWorks.TextAdventure.Engine.Blueprints.Messages.Lines;
using BoningerWorks.TextAdventure.Engine.Executables.Maps.Enums;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;

namespace BoningerWorks.TextAdventure.Engine.Executables.Maps
{
	public class LineMap
	{
		public ELineMapType Type { get; }
		public LineInlinedMap Inlined { get; }
		public Symbol Input { get; }

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
			// Check if input
			if (lineBlueprint.Input != null)
			{
				// Increase count
				count++;
				// Set type
				Type = ELineMapType.Input;
				// Set input
				Input = new Symbol(lineBlueprint.Input);
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

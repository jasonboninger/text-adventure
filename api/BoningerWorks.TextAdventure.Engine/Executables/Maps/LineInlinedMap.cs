using BoningerWorks.TextAdventure.Engine.Blueprints.Messages.Lines;
using System;

namespace BoningerWorks.TextAdventure.Engine.Executables.Maps
{
	public class LineInlinedMap
	{
		public string Text { get; }

		public LineInlinedMap(LineInlinedBlueprint lineInlinedBlueprint)
		{
			// Check if inlined line blueprint does not exist
			if (lineInlinedBlueprint == null)
			{
				// Throw error
				throw new ArgumentException("Inlined line blueprint cannot be null.", nameof(lineInlinedBlueprint));
			}
			// Check if text does not exist
			if (string.IsNullOrWhiteSpace(lineInlinedBlueprint.Text))
			{
				// Throw error
				throw new ArgumentException("Text cannot be null, empty, or whitespace.");
			}
			// Set text
			Text = lineInlinedBlueprint.Text;
		}
	}
}

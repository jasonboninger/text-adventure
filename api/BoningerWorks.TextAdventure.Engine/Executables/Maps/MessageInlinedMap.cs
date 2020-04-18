using BoningerWorks.TextAdventure.Engine.Blueprints.Messages;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables.Maps
{
	public class MessageInlinedMap
	{
		public ImmutableArray<LineMap> LineMaps { get; }

		public MessageInlinedMap(MessageInlinedBlueprint messageInlinedBlueprint)
		{
			// Check if inlined message blueprint does not exist
			if (messageInlinedBlueprint == null)
			{
				// Throw error
				throw new ArgumentException("Inlined message blueprint cannot be null.", nameof(messageInlinedBlueprint));
			}
			// Check if lines does not exist
			if (messageInlinedBlueprint.Lines == null || messageInlinedBlueprint.Lines.Count == 0)
			{
				// Throw error
				throw new ArgumentException("Line blueprints cannot be empty.");
			}
			// Set line maps
			LineMaps = messageInlinedBlueprint.Lines.Select(l => new LineMap(l)).ToImmutableArray();
		}
	}
}

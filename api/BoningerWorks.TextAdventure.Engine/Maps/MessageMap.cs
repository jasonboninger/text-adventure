using BoningerWorks.TextAdventure.Engine.Json.Serializable;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Maps
{
	public class MessageMap
	{
		public ImmutableArray<LineMap> LineMaps { get; }

		public MessageMap(MessageBlueprint messageBlueprint)
		{
			// Check if message blueprint does not exist
			if (messageBlueprint == null)
			{
				// Throw error
				throw new ArgumentException("Message blueprint cannot be null.", nameof(messageBlueprint));
			}
			// Check if lines does not exist
			if (messageBlueprint.Lines == null || messageBlueprint.Lines.Count == 0)
			{
				// Throw error
				throw new ArgumentException("Line blueprints cannot be empty.");
			}
			// Set line maps
			LineMaps = messageBlueprint.Lines.Select(l => new LineMap(l)).ToImmutableArray();
		}
	}
}

using BoningerWorks.TextAdventure.Engine.Json.Serializable;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Maps
{
	public class ActionMap
	{
		public EActionMapType Type { get; }
		public ImmutableArray<MessageMap> MessageMaps { get; }

		public ActionMap(ActionBlueprint actionBlueprint)
		{
			// Check if action blueprint does not exist
			if (actionBlueprint == null)
			{
				// Throw error
				throw new ArgumentException("Action blueprint cannot be null.", nameof(actionBlueprint));
			}
			// Create count
			var count = 0;
			// Check if messages exists
			if (actionBlueprint.Messages != null)
			{
				// Increase count
				count++;
				// Set type
				Type = EActionMapType.Messages;
				// Check if messages is empty
				if (actionBlueprint.Messages.Count == 0)
				{
					// Throw error
					throw new ArgumentException("Message blueprints cannot be empty.");
				}
				// Set message maps
				MessageMaps = actionBlueprint.Messages.Select(m => new MessageMap(m)).ToImmutableArray();
			}
			// Check if count is not one
			if (count != 1)
			{
				// Throw error
				throw new ArgumentException($"Action blueprint must have exactly one action, but instead has {count}.", nameof(actionBlueprint));
			}
		}
	}
}

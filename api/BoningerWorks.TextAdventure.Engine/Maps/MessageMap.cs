using BoningerWorks.TextAdventure.Engine.Blueprints.Messages;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;

namespace BoningerWorks.TextAdventure.Engine.Maps
{
	public class MessageMap
	{
		public EMessageMapType Type { get; }
		public MessageInlinedMap Inlined { get; }
		public Symbol Input { get; }

		public MessageMap(MessageBlueprint messageBlueprint)
		{
			// Check if message bluprint does not exist
			if (messageBlueprint == null)
			{
				// Throw error
				throw new ArgumentException("Message blueprint cannot be null.", nameof(messageBlueprint));
			}
			// Create count
			var count = 0;
			// Check if inlined
			if (messageBlueprint.Inlined != null)
			{
				// Increase count
				count++;
				// Set type
				Type = EMessageMapType.Inlined;
				// Set inlined
				Inlined = new MessageInlinedMap(messageBlueprint.Inlined);
			}
			// Check if input
			if (messageBlueprint.Input != null)
			{
				// Increase count
				count++;
				// Set type
				Type = EMessageMapType.Input;
				// Set input
				Input = new Symbol(messageBlueprint.Input);
			}
			// Check if count is not one
			if (count != 1)
			{
				// Throw error
				throw new ArgumentException($"Message blueprint must have exactly one value, but instead has {count}.", nameof(messageBlueprint));
			}
		}
	}
}

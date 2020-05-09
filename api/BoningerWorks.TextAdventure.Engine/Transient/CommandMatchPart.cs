using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Engine.Structural;
using System;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Transient
{
	public class CommandMatchPart
	{
		public CommandInput Input { get; }
		public IReadOnlyList<IEntity> Entities { get; }

		public CommandMatchPart(CommandInput input, IReadOnlyList<IEntity> entities)
		{
			// Set input
			Input = input ?? throw new ArgumentException("Input cannot be null.", nameof(input));
			// Check if entities does not exist
			if (entities == null || entities.Count == 0)
			{
				// Throw error
				throw new ArgumentException("Entities cannot be null or empty.", nameof(entities));
			}
			// Set entities
			Entities = entities;
		}
	}
}

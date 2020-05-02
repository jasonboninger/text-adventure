using BoningerWorks.TextAdventure.Engine.Interfaces;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class CommandMatchPart
	{
		public CommandInput Input { get; }
		public ImmutableList<IEntity> Entities { get; }

		public CommandMatchPart(CommandInput input, ImmutableList<IEntity> entities)
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

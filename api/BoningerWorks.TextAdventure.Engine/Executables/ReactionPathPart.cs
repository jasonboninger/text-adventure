using BoningerWorks.TextAdventure.Engine.Interfaces;
using System;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ReactionPathPart
	{
		public CommandInput Input { get; }
		public IEntity Entity { get; }

		public ReactionPathPart(CommandInput input, IEntity entity)
		{
			// Set input
			Input = input ?? throw new ArgumentException("Input cannot be null.", nameof(input));
			// Set entity
			Entity = entity ?? throw new ArgumentException("Entity cannot be null.", nameof(entity));
		}
	}
}

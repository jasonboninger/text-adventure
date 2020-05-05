using BoningerWorks.TextAdventure.Engine.Interfaces;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ReactionQuery
	{
		public Command Command { get; }
		public ImmutableList<IEntity> Parts { get; }
		
		public ReactionQuery(Command command, ImmutableList<IEntity> parts)
		{
			// Set command
			Command = command ?? throw new ArgumentException("Command cannot be null.", nameof(command));
			// Set parts
			Parts = parts ?? throw new ArgumentException("Parts cannot be null.", nameof(parts));
		}
	}
}

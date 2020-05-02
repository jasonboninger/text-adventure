using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ReactionPath
	{
		public Command Command { get; }
		public ImmutableList<ReactionPathPart> Parts { get; }

		public ReactionPath(Command command, ImmutableList<ReactionPathPart> parts)
		{
			// Set command
			Command = command ?? throw new ArgumentException("Command cannot be null.", nameof(command));
			// Set parts
			Parts = parts ?? throw new ArgumentException("Parts cannot be null.", nameof(parts));
		}
	}
}

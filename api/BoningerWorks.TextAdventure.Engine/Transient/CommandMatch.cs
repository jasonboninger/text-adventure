using BoningerWorks.TextAdventure.Engine.Structural;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Transient
{
	public class CommandMatch
	{
		public Command Command { get; }
		public ImmutableList<CommandMatchPart> Parts { get; }

		public CommandMatch(Command command, ImmutableList<CommandMatchPart> parts)
		{
			// Set command
			Command = command ?? throw new ArgumentException("Command cannot be null.", nameof(command));
			// Set parts
			Parts = parts ?? throw new ArgumentException("Parts cannot be null.", nameof(parts));
		}
	}
}

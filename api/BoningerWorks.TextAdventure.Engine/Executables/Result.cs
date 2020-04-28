using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Result
	{
		public State State { get; }
		public ImmutableList<Message> Messages { get; }

		public Result(State state, ImmutableList<Message> messages)
		{
			// Set state
			State = state ?? throw new ArgumentException("State cannot be null.", nameof(state));
			// Set messages
			Messages = messages;
		}
	}
}

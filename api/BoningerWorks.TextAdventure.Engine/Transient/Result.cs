using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Transient
{
	public class Result
	{
		public State State { get; }
		public ImmutableList<Message> MessagesEffect { get; }
		public ImmutableList<Message> MessagesPrompt { get; }

		public Result(State state, ImmutableList<Message> messagesEffect, ImmutableList<Message> messagesPrompt)
		{
			// Set state
			State = state ?? throw new ArgumentException("State cannot be null.", nameof(state));
			// Set effect messages
			MessagesEffect = messagesEffect;
			// Set prompt messages
			MessagesPrompt = messagesPrompt;
		}
	}
}

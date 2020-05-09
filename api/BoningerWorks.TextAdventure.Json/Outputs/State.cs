using BoningerWorks.TextAdventure.Core.Utilities;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class State
	{
		public ImmutableDictionary<Id, Entity> Entities { get; }
		public ImmutableList<Message> Prompt { get; }
		public bool Complete { get; }

		public State(ImmutableDictionary<Id, Entity> entityStates, ImmutableList<Message> prompt, bool complete)
		{
			// Set entities
			Entities = entityStates ?? ImmutableDictionary<Id, Entity>.Empty;
			// Set prompt
			Prompt = prompt;
			// Set complete
			Complete = complete;
		}

		public State UpdateEntity(Id entityId, Entity entityState)
		{
			// Check if entity does not exist
			if (!Entities.ContainsKey(entityId))
			{
				// Throw error
				throw new ArgumentException($"Entity with ID ({entityId}) could not be found.", nameof(entityId));
			}
			// Set entity
			var entities = Entities.SetItem(entityId, entityState);
			// Create state
			var state = new State(entities, Prompt, Complete);
			// Return state
			return state;
		}

		public State SetPrompt(ImmutableList<Message> prompt)
		{
			// Create state
			var state = new State(Entities, prompt, Complete);
			// Return state
			return state;
		}

		public State UpdateComplete(bool complete)
		{
			// Create state
			var state = new State(Entities, Prompt, complete);
			// Return state
			return state;
		}
	}
}

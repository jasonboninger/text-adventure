using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Reactions : IReadOnlyList<Reaction>
	{
		public Reaction this[int index] => _reactions[index];
		public int Count => _reactions.Length;

		private readonly Entities _entities;
		private readonly Commands _commands;
		private readonly ImmutableArray<ReactionPath> _reactionPaths;
		private readonly ImmutableArray<Reaction> _reactions;
		private readonly IEnumerable<Reaction> _reactionsEnumerable;
		private readonly ImmutableDictionary<Command, ReactionTree> _commandToReactionTreeMappings;

		public Reactions(Entities entities, Commands commands, ImmutableArray<ReactionMap> reactionMaps)
		{
			// Set entities
			_entities = entities;
			// Set commands
			_commands = commands;
			// Create triggers
			var triggers = new Triggers();
			// Set reaction paths
			_reactionPaths = reactionMaps
				.Select(rm => new ReactionPath(_entities, _commands, rm))
				.ToImmutableArray();
			// Set reactions
			_reactions = reactionMaps
				.Select((rm, i) => new Reaction(triggers, _entities, _commands, _reactionPaths, _reactionPaths[i], rm.ActionMaps))
				.ToImmutableArray();
			// Validate trigger paths
			triggers.Validate();
			// Set enumerable reactions
			_reactionsEnumerable = _reactions;
			// Set command symbol to reaction tree mappings
			_commandToReactionTreeMappings = ReactionTree.Create(_reactions);
		}

		IEnumerator IEnumerable.GetEnumerator() => _reactionsEnumerable.GetEnumerator();
		public IEnumerator<Reaction> GetEnumerator() => _reactionsEnumerable.GetEnumerator();

		public ImmutableArray<Reaction> GetMatches(ReactionQuery reactionQuery)
		{
			// Check if reaction query does not exist
			if (reactionQuery == null)
			{
				// Return no reactions
				return ImmutableArray<Reaction>.Empty;
			}
			// Get command
			var command = reactionQuery.Command;
			// Try to get reaction tree
			if (!_commandToReactionTreeMappings.TryGetValue(command, out var reactionTree))
			{
				// Return no reactions
				return ImmutableArray<Reaction>.Empty;
			}
			// Create reactions
			var reactions = reactionTree.Reactions;
			// Get parts
			var parts = reactionQuery.Parts;
			// Run through parts
			for (int i = 0; i < parts.Count; i++)
			{
				var part = parts[i];
				// Try to get next reaction tree
				if (reactionTree.Next == null || !reactionTree.Next.TryGetValue(part, out reactionTree))
				{
					// Return no reactions
					return ImmutableArray<Reaction>.Empty;
				}
				// Set reactions
				reactions = reactionTree.Reactions;
			}
			// Return reactions
			return reactions ?? ImmutableArray<Reaction>.Empty;
		}

		public Action<ResultBuilder> CreateAction(ImmutableArray<ActionMap> actionMaps, Func<Symbol, Symbol>? replacer = null)
		{
			// Create action
			var action = Actions.Create(triggers: null, _entities, _commands, _reactionPaths, reactionPath: null, actionMaps, replacer);
			// Return action
			return action;
		}
	}
}

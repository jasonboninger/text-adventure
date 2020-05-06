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

		private readonly ImmutableArray<Reaction> _reactions;
		private readonly IEnumerable<Reaction> _reactionsEnumerable;
		private readonly ImmutableDictionary<Command, ReactionTree> _commandToReactionTreeMappings;
		private readonly Action<ResultBuilder> _actionStart;
		private readonly Action<ResultBuilder> _actionEnd;
		private readonly Action<ResultBuilder> _actionFail;

		public Reactions
		(
			Entities entities, 
			Commands commands, 
			ImmutableArray<ReactionMap> reactionMaps,
			ImmutableArray<ActionMap> actionMapsStart,
			ImmutableArray<ActionMap> actionMapsEnd,
			ImmutableArray<ActionMap> actionMapsFail
		)
		{
			// Create triggers
			var triggers = new Triggers();
			// Create reaction paths
			var reactionPaths = reactionMaps
				.Select(rm => new ReactionPath(entities, commands, rm))
				.ToImmutableArray();
			// Set reactions
			_reactions = reactionMaps
				.Select((rm, i) => new Reaction(triggers, entities, commands, reactionPaths, reactionPaths[i], rm.ActionMaps))
				.ToImmutableArray();
			// Validate trigger paths
			triggers.Validate();
			// Set enumerable reactions
			_reactionsEnumerable = _reactions;
			// Set command symbol to reaction tree mappings
			_commandToReactionTreeMappings = ReactionTree.Create(_reactions);
			// Set start action
			_actionStart = Actions.Create(triggers: null, entities, commands, reactionPaths, reactionPath: null, actionMapsStart);
			// Set end action
			_actionEnd = Actions.Create(triggers: null, entities, commands, reactionPaths, reactionPath: null, actionMapsEnd);
			// Set fail action
			_actionFail = Actions.Create(triggers: null, entities, commands, reactionPaths, reactionPath: null, actionMapsFail);
		}

		IEnumerator IEnumerable.GetEnumerator() => _reactionsEnumerable.GetEnumerator();
		public IEnumerator<Reaction> GetEnumerator() => _reactionsEnumerable.GetEnumerator();

		public ImmutableArray<Reaction> Match(ReactionQuery reactionQuery)
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

		public void ExecuteStart(ResultBuilder result)
		{
			// Execute start action
			_actionStart(result);
		}

		public void ExecuteEnd(ResultBuilder result)
		{
			// Execute end action
			_actionEnd(result);
		}

		public void ExecuteFail(ResultBuilder result)
		{
			// Execute fail action
			_actionFail(result);
		}
	}
}

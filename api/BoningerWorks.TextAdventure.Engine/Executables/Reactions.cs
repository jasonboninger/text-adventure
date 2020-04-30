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
		public int Count => _reactions.Length;
		public Reaction this[int index] => _reactions[index];

		private readonly ImmutableArray<Reaction> _reactions;
		private readonly IEnumerable<Reaction> _reactionsEnumerable;
		private readonly ImmutableDictionary<Symbol, ReactionTree> _commandSymbolToReactionTreeMappings;

		public Reactions(Entities entities, Items items, Commands commands, ImmutableArray<ReactionMap> reactionMaps)
		{
			// Set reactions
			_reactions = reactionMaps.Select(rm => new Reaction(entities, items, commands, rm)).ToImmutableArray();
			// Set enumerable reactions
			_reactionsEnumerable = _reactions;
			// Set command symbol to reaction tree mappings
			_commandSymbolToReactionTreeMappings = ReactionTree.Create(_reactions);
		}

		public IEnumerator<Reaction> GetEnumerator() => _reactionsEnumerable.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _reactionsEnumerable.GetEnumerator();

		public ReactionTree Get(Command command)
		{
			// Try to get reaction tree
			var reactionTree = TryGet(command);
			// Check if reaction tree does not exist
			if (reactionTree == null)
			{
				// Throw error
				throw new ArgumentException($"No reaction for command ({command}) could be found.");
			}
			// Return reaction tree
			return reactionTree;
		}

		public ReactionTree? TryGet(Command? command)
		{
			// Try to get reaction tree
			if (command != null && command.Symbol != null && _commandSymbolToReactionTreeMappings.TryGetValue(command.Symbol, out var reactionTree))
			{
				// Return reaction tree
				return reactionTree;
			}
			// Return no reaction tree
			return null;
		}
	}
}

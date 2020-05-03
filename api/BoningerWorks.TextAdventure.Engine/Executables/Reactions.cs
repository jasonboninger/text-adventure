using BoningerWorks.TextAdventure.Intermediate.Maps;
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

		public Reactions(Entities entities, Commands commands, ImmutableArray<ReactionMap> reactionMaps)
		{
			// Set reactions
			_reactions = reactionMaps.Select(rm => new Reaction(entities, commands, rm)).ToImmutableArray();
			// Set enumerable reactions
			_reactionsEnumerable = _reactions;
			// Set command symbol to reaction tree mappings
			_commandToReactionTreeMappings = ReactionTree.Create(_reactions);
		}

		IEnumerator IEnumerable.GetEnumerator() => _reactionsEnumerable.GetEnumerator();
		public IEnumerator<Reaction> GetEnumerator() => _reactionsEnumerable.GetEnumerator();

		public ImmutableArray<Reaction>? TryGet(ReactionPath? reactionPath)
		{
			// Check if reaction path does not exist
			if (reactionPath == null)
			{
				// Return no reactions
				return null;
			}
			// Get command
			var command = reactionPath.Command;
			// Try to get reaction tree
			if (!_commandToReactionTreeMappings.TryGetValue(command, out var reactionTree))
			{
				// Return no reactions
				return null;
			}
			// Create reactions
			var reactions = reactionTree.Reactions;
			// Get parts
			var parts = reactionPath.Parts;
			// Run through parts
			for (int i = 0; i < parts.Count; i++)
			{
				var part = parts[i];
				// Try to get next reaction tree
				if (reactionTree.Next == null || !reactionTree.Next.TryGetValue(part.Entity, out reactionTree))
				{
					// Return no reactions
					return null;
				}
				// Set reactions
				reactions = reactionTree.Reactions;
			}
			// Return reactions
			return reactions;
		}
	}
}

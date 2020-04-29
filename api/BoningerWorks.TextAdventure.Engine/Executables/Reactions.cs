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

		private readonly ImmutableDictionary<Symbol, Reaction> _commandSymbolToReactionMappings;
		private readonly ImmutableArray<Reaction> _reactions;
		private readonly IEnumerable<Reaction> _reactionsEnumerable;

		public Reactions(Entities entities, Items items, Commands commands, ImmutableArray<ReactionMap> reactionMaps)
		{
			// Set command symbol to command handler mappings
			_commandSymbolToReactionMappings = reactionMaps
				.GroupBy
					(
						rm => rm.CommandSymbol,
						(cs, rms) =>
						{
							// Try to get command
							var command = commands.TryGet(cs);
							// Check if command does not exist
							if (command == null)
							{
								// Throw error
								throw new InvalidOperationException($"No command with symbol ({cs}) could be found.");
							}
							// Create reaction
							var reaction = new Reaction(entities, items, command, rms.ToImmutableList());
							// Return command symbol to reaction mapping
							return KeyValuePair.Create(cs, reaction);
						}
					)
				.ToImmutableDictionary();
			// Set reactions
			_reactions = _commandSymbolToReactionMappings.Values.ToImmutableArray();
			// Set enumerable reactions
			_reactionsEnumerable = _reactions;
		}

		public IEnumerator<Reaction> GetEnumerator() => _reactionsEnumerable.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _reactionsEnumerable.GetEnumerator();

		public Reaction Get(Command command)
		{
			// Try to get reaction
			var reaction = TryGet(command);
			// Check if reaction does not exist
			if (reaction == null)
			{
				// Throw error
				throw new ArgumentException($"No reaction for command ({command}) could be found.");
			}
			// Return reaction
			return reaction;
		}

		public Reaction? TryGet(Command? command)
		{
			// Try to get reaction
			if (command != null && command.Symbol != null && _commandSymbolToReactionMappings.TryGetValue(command.Symbol, out var reaction))
			{
				// Return reaction
				return reaction;
			}
			// Return no reaction
			return null;
		}
	}
}

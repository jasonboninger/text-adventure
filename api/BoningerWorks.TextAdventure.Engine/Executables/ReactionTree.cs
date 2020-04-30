using BoningerWorks.TextAdventure.Core.Utilities;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ReactionTree
	{
		public static ImmutableDictionary<Symbol, ReactionTree> Create(ImmutableArray<Reaction> reactions)
		{
			// Return command symbol to reaction tree mappings
			return reactions
				.GroupBy(r => r.Command, (c, rs) => KeyValuePair.Create(c.Symbol, new ReactionTree(c, rs.ToImmutableList(), index: 0)))
				.ToImmutableDictionary();
		}

		public ImmutableDictionary<Symbol, ReactionTree>? Next { get; }
		public ImmutableArray<Reaction>? Reactions { get; }

		private ReactionTree(Command command, ImmutableList<Reaction> reactions, int index)
		{
			// Check if no more command item symbols
			if (index == command.CommandItems.Length)
			{
				// Set reactions
				Reactions = reactions.ToImmutableArray();
			}
			else
			{
				// Set next
				Next = _CreateNext(command, reactions, index);
			}
		}

		private static ImmutableDictionary<Symbol, ReactionTree> _CreateNext(Command command, ImmutableList<Reaction> reactions, int index)
		{
			// Get next index
			var indexNext = index + 1;
			// Create next
			var next = reactions
				.GroupBy
					(
						m =>
						{
							// Get item
							var item = m.CommandItemToItemMappings[command.CommandItems[index]];
							// Return item symbol
							return item.Symbol;
						},
						(@is, m) => KeyValuePair.Create(@is, new ReactionTree(command, m.ToImmutableList(), indexNext))
					)
				.ToImmutableDictionary();
			// Return next
			return next;
		}
	}
}

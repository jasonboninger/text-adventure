using BoningerWorks.TextAdventure.Engine.Comparers;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ReactionTree
	{
		public static ImmutableDictionary<Command, ReactionTree> Create(ImmutableArray<Reaction> reactions)
		{
			// Return command to reaction tree mappings
			return reactions
				.GroupBy(r => r.Command, (c, rs) => KeyValuePair.Create(c, new ReactionTree(c, rs.ToImmutableList(), index: 0)))
				.ToImmutableDictionary(IdentifiableEqualityComparer<Command>.Instance);
		}

		public ImmutableDictionary<IEntity, ReactionTree>? Next { get; }
		public ImmutableArray<Reaction>? Reactions { get; }

		private ReactionTree(Command command, ImmutableList<Reaction> reactions, int index)
		{
			// Check if no more command inputs
			if (index == command.Inputs.Length)
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

		private static ImmutableDictionary<IEntity, ReactionTree> _CreateNext(Command command, ImmutableList<Reaction> reactions, int index)
		{
			// Get next index
			var indexNext = index + 1;
			// Create next
			var next = reactions
				.GroupBy
					(
						r => r.Path.Parts[index].Entity,
						(e, r) => KeyValuePair.Create(e, new ReactionTree(command, r.ToImmutableList(), indexNext))
					)
				.ToImmutableDictionary(IdentifiableEqualityComparer<IEntity>.Instance);
			// Return next
			return next;
		}
	}
}

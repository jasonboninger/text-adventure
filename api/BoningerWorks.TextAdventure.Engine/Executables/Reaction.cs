using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Reaction
	{
		public Command Command { get; }
		public ImmutableDictionary<Symbol, Reaction>? Next { get; }
		public ImmutableArray<ReactionMatch>? Matches { get; }

		public Reaction(Entities entities, Items items, Command command, ImmutableList<ReactionMap> reactionMaps) 
		: this
		(
			command,
			reactionMaps
				.Select
					(rm =>
					{
						// Check if command does not match reaction map command
						if (command.Symbol != rm.CommandSymbol)
						{
							// Throw error
							throw new ArgumentException($"Command ({command}) does not match command ({rm.CommandSymbol}) of reaction map.");
						}
						// Create match
						var match = new ReactionMatch(entities, items, command, rm);
						// Return match
						return match;
					})
				.ToImmutableList(),
			index: 0
		) 
		{ }
		private Reaction(Command command, ImmutableList<ReactionMatch> matches, int index)
		{
			// Set command
			Command = command;
			// Check if no more command item symbols
			if (index == Command.CommandItems.Length)
			{
				// Set matches
				Matches = matches.ToImmutableArray();
			}
			else
			{
				// Set next
				Next = _CreateNext(Command, matches, index);
			}
		}

		private static ImmutableDictionary<Symbol, Reaction> _CreateNext(Command command, ImmutableList<ReactionMatch> matches, int index)
		{
			// Get next index
			var indexNext = index + 1;
			// Create next
			var next = matches
				.GroupBy
					(
						m =>
						{
							// Get item
							var item = m.CommandItemToItemMappings[command.CommandItems[index]];
							// Return item symbol
							return item.Symbol;
						},
						(@is, m) => KeyValuePair.Create(@is, new Reaction(command, m.ToImmutableList(), indexNext))
					)
				.ToImmutableDictionary();
			// Return next
			return next;
		}
	}
}

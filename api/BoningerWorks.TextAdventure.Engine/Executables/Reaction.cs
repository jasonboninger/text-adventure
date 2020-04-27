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
		public ImmutableArray<Action>? Actions { get; }

		public Reaction(Player player, Areas areas, Items items, Command command, ImmutableList<ReactionMap> reactionMaps) 
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
						// Create action
						var action = new Action(player, areas, items, command, rm);
						// Return action
						return action;
					})
				.ToImmutableList(),
			index: 0
		) 
		{ }
		private Reaction(Command command, ImmutableList<Action> actions, int index)
		{
			// Set command
			Command = command;
			// Check if no more command item symbols
			if (index == Command.CommandItems.Length)
			{
				// Set actions
				Actions = actions.ToImmutableArray();
			}
			else
			{
				// Set next
				Next = _CreateNext(Command, actions, index);
			}
		}

		private static ImmutableDictionary<Symbol, Reaction> _CreateNext(Command command, ImmutableList<Action> actions, int index)
		{
			// Get next index
			var indexNext = index + 1;
			// Create next
			var next = actions
				.GroupBy
					(
						a =>
						{
							// Get item
							var item = a.CommandItemToItemMappings[command.CommandItems[index]];
							// Return item symbol
							return item.Symbol;
						},
						(@is, @as) => KeyValuePair.Create(@is, new Reaction(command, @as.ToImmutableList(), indexNext))
					)
				.ToImmutableDictionary();
			// Return next
			return next;
		}
	}
}

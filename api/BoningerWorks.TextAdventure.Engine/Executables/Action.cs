using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Action
	{
		public Command Command { get; }
		public ImmutableDictionary<Symbol, Item> CommandItemToItemMappings { get; }

		private readonly ImmutableArray<Action<ResultBuilder>> _actions;

		public Action(Entities entities, Items items, Command command, ReactionMap reactionMap)
		{
			// Check if command does not match reaction map command
			if (command.Symbol != reactionMap.CommandSymbol)
			{
				// Throw error
				throw new ArgumentException($"Command ({command}) does not match command ({reactionMap.CommandSymbol}) of reaction map.");
			}
			// Set command
			Command = command;
			// Check if reaction map has no command item symbol to item symbol mappings
			if (reactionMap.CommandItemSymbolToItemSymbolMappings.Count == 0)
			{
				// Check if command has more than one item symbol
				if (Command.CommandItems.Length > 1)
				{
					// Throw error
					throw new ValidationError($"Reaction map for command ({Command}) is not valid.");
				}
			}
			else
			{
				// Check if command map does not have an item symbol for each command item symbol
				if (command.CommandItems.Any(ci => !reactionMap.CommandItemSymbolToItemSymbolMappings.ContainsKey(ci)))
				{
					// Throw error
					throw new ValidationError($"Reaction map for command ({Command}) is not valid.");
				}
			}
			// Set command item symbol to item symbol mappings
			CommandItemToItemMappings = Command.CommandItems
				.Select
					(cis =>
					{
						// Try to get item symbol for command item symbol
						if (!reactionMap.CommandItemSymbolToItemSymbolMappings.TryGetValue(cis, out var itemSymbol))
						{
							// Set item symbol to default item symbol
							itemSymbol = reactionMap.ItemSymbolDefault 
								?? throw new ValidationError($"Reaction map for command ({Command}) is not valid.");
						}
						// Check if item symbol does not exist
						if (itemSymbol == null)
						{
							// Throw error
							throw new ValidationError($"No mapping could be found in command map for command item ({cis}) of command ({Command}).");
						}
						// Check if item does not exist
						if (!items.Contains(itemSymbol))
						{
							// Throw error
							throw new ValidationError($"No item with symbol ({itemSymbol}) could be found.");
						}
						// Return command item to item mapping
						return KeyValuePair.Create(cis, items.Get(itemSymbol));
					})
				.ToImmutableDictionary();
			// Set actions
			_actions = reactionMap.ActionMaps
				.SelectMany
					(am =>
					{
						// Check if if map exists
						if (am.IfMap != null)
						{

							return Enumerable.Empty<Action<ResultBuilder>>();

						}
						// Check if message maps exist
						if (am.MessageMaps.HasValue)
						{
							// Return message actions
							return am.MessageMaps.Value.Select(mm => ActionMessage.Create(mm));
						}
						// Check if change maps exist
						if (am.ChangeMaps.HasValue)
						{
							// Return change actions
							return am.ChangeMaps.Value.Select(cm => ActionChange.Create(entities, cm));
						}
						// Check if trigger maps exist
						if (am.TriggerMaps.HasValue)
						{

							return Enumerable.Empty<Action<ResultBuilder>>();

						}
						// Throw error
						throw new InvalidOperationException("Action map could not be parsed.");
					})
				.ToImmutableArray();
		}

		public Result Execute(State state)
		{
			// Create result
			var result = new ResultBuilder(state);
			// Run through actions
			for (int i = 0; i < _actions.Length; i++)
			{
				// Execute action
				_actions[i](result);
			}
			// Return result
			return result.ToImmutable();
		}
	}
}

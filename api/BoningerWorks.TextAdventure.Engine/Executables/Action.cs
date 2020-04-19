using BoningerWorks.TextAdventure.Engine.Maps;
using BoningerWorks.TextAdventure.Engine.States;
using BoningerWorks.TextAdventure.Engine.States.Messages;
using BoningerWorks.TextAdventure.Engine.States.Messages.Lines;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Action
	{
		public Command Command { get; }
		public ImmutableDictionary<Symbol, Item> CommandItemSymbolToItemMappings { get; }

		private readonly ImmutableArray<Func<GameState, List<MessageState>>> _executes;

		public Action(Items items, Command command, CommandMap commandMap)
		{
			// Check if command does not match command map
			if (command.Symbol != commandMap.CommandSymbol)
			{
				// Throw error
				throw new ArgumentException($"Command ({command}) does not match command ({commandMap.CommandSymbol}) of command map.");
			}
			// Set command
			Command = command;
			// Check if command map has no command item symbol to item symbol mappings
			if (commandMap.CommandItemSymbolToItemSymbolMappings.Count == 0)
			{
				// Check if command has more than one item symbol
				if (Command.ItemSymbols.Length > 1)
				{
					// Throw error
					throw new ArgumentException($"Command map for command ({Command}) is not valid.", nameof(commandMap));
				}
			}
			else
			{
				// Check if command map does not have an item symbol for each command item symbol
				if (command.ItemSymbols.Any(cis => !commandMap.CommandItemSymbolToItemSymbolMappings.ContainsKey(cis)))
				{
					// Throw error
					throw new ArgumentException($"Command map for command ({Command}) is not valid.", nameof(commandMap));
				}
			}
			// Set command item symbol to item symbol mappings
			CommandItemSymbolToItemMappings = Command.ItemSymbols
				.Select
					(cis =>
					{
						// Try to get item symbol for command item symbol
						if (!commandMap.CommandItemSymbolToItemSymbolMappings.TryGetValue(cis, out var itemSymbol))
						{
							// Set item symbol to default item symbol
							itemSymbol = commandMap.ItemSymbolDefault;
						}
						// Check if item symbol does not exist
						if (itemSymbol == null)
						{
							// Create message
							var message = $"No mapping could be found in command map for command item symbol ({cis}) of command ({Command}).";
							// Throw error
							throw new InvalidOperationException(message);
						}
						// Check if item does not exist
						if (!items.Contains(itemSymbol))
						{
							// Throw error
							throw new InvalidOperationException($"No item with symbol ({itemSymbol}) could be found.");
						}
						// Return command item symbol to item mapping
						return KeyValuePair.Create(cis, items.Get(itemSymbol));
					})
				.ToImmutableDictionary();
			// Set executes
			_executes = commandMap.ActionMaps.Select(_CreateExecute).ToImmutableArray();
		}

		public List<MessageState> Execute(GameState gameState)
		{
			// Create messages
			var messages = new List<MessageState>();
			// Run through executes
			for (int i = 0; i < _executes.Length; i++)
			{
				// Execute execute
				messages.AddRange(_executes[i](gameState));
			}
			// Return messages
			return messages;
		}

		private Func<GameState, List<MessageState>> _CreateExecute(ActionMap actionMap)
		{
			// Check action type
			switch (actionMap.Type)
			{
				case EActionMapType.If: throw new NotImplementedException();
				case EActionMapType.Messages:
					// Create message executes
					var executesMessage = actionMap.MessageMaps.Select(_CreateExecuteMessage).ToImmutableArray();
					// Return execute
					return gameState =>
					{
						// Create messages
						var messages = new List<MessageState>();
						// Run through message executes
						for (int i = 0; i < executesMessage.Length; i++)
						{
							// Add message
							messages.Add(executesMessage[i](gameState));
						}
						// Return messages
						return messages;
					};
				case EActionMapType.Changes: throw new NotImplementedException();
				case EActionMapType.Triggers: throw new NotImplementedException();
				default: throw new ArgumentException($"Action map type ({actionMap.Type}) could not be handled.");
			}
		}

		private Func<GameState, MessageState> _CreateExecuteMessage(MessageMap messageMap)
		{
			// Check message type
			switch (messageMap.Type)
			{
				case EMessageMapType.Inlined:
					// Create line executes
					var executesLine = messageMap.Inlined.LineMaps.Select(_CreateExecuteLine).ToImmutableArray();
					// Return execute
					return gameState =>
					{
						// Create lines
						var lines = new List<LineState>();
						// Run through line executes
						for (int i = 0; i < executesLine.Length; i++)
						{
							// Add line
							lines.Add(executesLine[i](gameState));
						}
						// Return message
						return MessageState.Create(lines);
					};
				case EMessageMapType.Templated: throw new NotImplementedException();
				case EMessageMapType.Input: throw new NotImplementedException();
				default: throw new ArgumentException($"Message map type ({messageMap.Type}) could not be handled.");
			}
		}

		private Func<GameState, LineState> _CreateExecuteLine(LineMap lineMap)
		{
			// Check line type
			switch (lineMap.Type)
			{
				case ELineMapType.Inlined:
					// Create line content execute
					var executeLineContent = _CreateExecuteLineContent(lineMap.Inlined);
					// Return execute
					return gameState =>
					{
						// Return line
						return LineState.CreateContent(executeLineContent(gameState));
					};
				case ELineMapType.Special: throw new NotImplementedException();
				case ELineMapType.Input: throw new NotImplementedException();
				default: throw new ArgumentException($"Line map type ({lineMap.Type}) could not be handled.");
			}
		}

		private Func<GameState, LineContentState> _CreateExecuteLineContent(LineInlinedMap lineInlinedMap)
		{
			// Check if text does not exist
			if (string.IsNullOrWhiteSpace(lineInlinedMap.Text))
			{
				// Throw error
				throw new ArgumentException("Line text cannot be null, empty, or whitespace.", nameof(lineInlinedMap));
			}
			// Return execute
			return gameState =>
			{
				// Return content
				return LineContentState.Create(lineInlinedMap.Text);
			};
		}
	}
}

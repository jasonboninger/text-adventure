using BoningerWorks.TextAdventure.Core.Interfaces;
using BoningerWorks.TextAdventure.Core.Static;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Command : IIdentifiable
	{
		public Symbol Symbol { get; }
		public ImmutableArray<CommandInput> Inputs { get; }

		private readonly Areas _areas;
		private readonly Items _items;
		private readonly ImmutableArray<Symbol> _parts;
		private readonly ImmutableDictionary<Symbol, Names> _wordToNamesMappings;
		private readonly ImmutableArray<Symbol> _commandAreas;
		private readonly ImmutableArray<Symbol> _commandItems;
		private readonly Regex _regularExpression;
		private readonly ImmutableDictionary<Symbol, CommandInput> _commandInputToInputMappings;

		public Command(Areas areas, Items items, CommandMap commandMap)
		{
			// Set areas
			_areas = areas;
			// Set items
			_items = items;
			// Set symbol
			Symbol = commandMap.CommandSymbol;
			// Set parts
			_parts = commandMap.PartSymbols;
			// Set word to names mappings
			_wordToNamesMappings = commandMap.WordSymbolToWordNamesMappings;
			// Set command areas
			_commandAreas = commandMap.CommandAreaSymbols;
			// Set command items
			_commandItems = commandMap.CommandItemSymbols;
			// Set regular expression and command input to input mappings
			(_regularExpression, _commandInputToInputMappings) = _CreateRegularExpressionAndCommandInputToInputMappings();
			// Set inputs
			Inputs = _commandInputToInputMappings.Values.ToImmutableArray();
		}

		public override string ToString()
		{
			// Return symbol
			return Symbol.ToString();
		}

		public CommandMatch? TryGetMatch(Game game, State state, string? input)
		{
			// Check if input exists
			if (input != null)
			{
				// Get match
				var match = _regularExpression.Match(input);
				// Check if match succeeded
				if (match.Success)
				{
					// Create parts
					var parts = ImmutableList.CreateBuilder<CommandMatchPart>();
					// Run through command areas
					for (int i = 0; i < _commandAreas.Length; i++)
					{
						var commandArea = _commandAreas[i];
						// Get command area group
						var commandAreaGroup = commandArea.ToString();
						// Get group
						var group = match.Groups[commandAreaGroup];
						// Get area name
						var areaName = new Name(group.Value);
						// Get areas
						var areas = _areas.GetAll(areaName);
						// Try to create part
						var part = _TryCreatePart(game, state, _commandInputToInputMappings[commandArea], areas);
						// Check if part does not exist
						if (part == null)
						{
							// Return no match
							return null;
						}
						// Add part
						parts.Add(part);
					}
					// Run through command items
					for (int i = 0; i < _commandItems.Length; i++)
					{
						var commandItem = _commandItems[i];
						// Get command item group
						var commandItemGroup = commandItem.ToString();
						// Get group
						var group = match.Groups[commandItemGroup];
						// Get item name
						var itemName = new Name(group.Value);
						// Get items
						var items = _items.GetAll(itemName);
						// Try to create part
						var part = _TryCreatePart(game, state, _commandInputToInputMappings[commandItem], items);
						// Check if part does not exist
						if (part == null)
						{
							// Return no match
							return null;
						}
						// Add part
						parts.Add(part);
					}
					// Return match
					return new CommandMatch(this, parts.ToImmutable());
				}
			}
			// Return no match
			return null;
		}

		private static CommandMatchPart? _TryCreatePart(Game game, State state, CommandInput input, IReadOnlyList<IEntity> entities)
		{
			// Create entities in context
			var entitiesInContext = ImmutableList.CreateBuilder<IEntity>();
			// Run through entities
			for (int i = 0; i < entities.Count; i++)
			{
				var entity = entities[i];
				// Check if entity is in context
				if (entity.IsInContext(game, state))
				{
					// Add entity in context
					entitiesInContext.Add(entity);
				}
			}
			// Check if entities in context exists
			if (entitiesInContext.Count > 0)
			{
				// Return part
				return new CommandMatchPart(input, entitiesInContext.ToImmutable());
			}
			// Return no part
			return null;
		}

		private Tuple<Regex, ImmutableDictionary<Symbol, CommandInput>> _CreateRegularExpressionAndCommandInputToInputMappings()
		{
			// Create command input to input mappings
			var commandInputToInputMappings = ImmutableDictionary.CreateBuilder<Symbol, CommandInput>();
			// Create regular expressions
			var regularExpressions = _parts.Select(p =>
			{
				// Check if word exists
				if (_wordToNamesMappings.TryGetValue(p, out var names))
				{
					// Create word regular expression
					var regularExpressionWord = RegularExpressions.CreateNonCapturingGroup(names.RegularExpression);
					// Return word regular expression
					return regularExpressionWord;
				}
				// Check if command area exists
				if (_commandAreas.Contains(p))
				{
					// Add input
					commandInputToInputMappings.Add(p, new CommandInput(p, e => e is Area));
					// Create command area regular expression
					var regularExpressionCommandArea = RegularExpressions.CreateCaptureGroup(p.ToString(), _areas.RegularExpression);
					// Return command area regular expression
					return regularExpressionCommandArea;
				}
				// Check if command item exists
				if (_commandItems.Contains(p))
				{
					// Add input
					commandInputToInputMappings.Add(p, new CommandInput(p, e => e is Item));
					// Create command item regular expression
					var regularExpressionCommandItem = RegularExpressions.CreateCaptureGroup(p.ToString(), _items.RegularExpression);
					// Return command item regular expression
					return regularExpressionCommandItem;
				}
				// Throw error
				throw new ValidationError($"Command ({Symbol}) part ({p}) could not be found in words or command items.");
			});
			// Create regular expression options
			var regularExpressionOptions = RegexOptions.IgnoreCase | RegexOptions.Singleline;
			// Create regular expression
			var regularExpression = new Regex(@"^" + string.Join(@" +", regularExpressions) + @"$", regularExpressionOptions);
			// Return regular expression and command input to input mappings
			return Tuple.Create(regularExpression, commandInputToInputMappings.ToImmutable());
		}
	}
}

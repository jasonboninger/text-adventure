using BoningerWorks.TextAdventure.Core.Interfaces;
using BoningerWorks.TextAdventure.Core.Static;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Command : IIdentifiable
	{
		private class CommandInputMetadatum
		{
			public CommandInput CommandInput { get; }
			public Func<Name, IReadOnlyList<IEntity>> GetEntitiesByName { get; }

			public CommandInputMetadatum(CommandInput commandInput, Func<Name, IReadOnlyList<IEntity>> getEntitiesByName)
			{
				// Set command input
				CommandInput = commandInput;
				// Set get entities by name
				GetEntitiesByName = getEntitiesByName;
			}
		}

		public Symbol Symbol { get; }
		public ImmutableArray<CommandInput> Inputs { get; }

		private readonly Regex _regularExpression;
		private readonly ImmutableArray<CommandInputMetadatum> _commandInputMetadata;
		private readonly ImmutableArray<ActionMap> _actionMapsFail;

		public Command(Entities entities, CommandMap commandMap)
		{
			// Set symbol
			Symbol = commandMap.CommandSymbol;
			// Create command input metadata
			var commandInputMetadata = ImmutableArray.CreateBuilder<CommandInputMetadatum>();
			// Create regular expressions
			var regularExpressions = new List<string>();
			// Run through command part maps
			foreach (var commandPartMap in commandMap.CommandPartMaps)
			{
				// Check if words exists
				if (commandPartMap.Words != null)
				{
					// Get words
					var words = commandPartMap.Words;
					// Create regular expression
					var regularExpression = RegularExpressions.CreateNonCapturingGroup(words.RegularExpression);
					// Add regular expression
					regularExpressions.Add(regularExpression);
					// Continue
					continue;
				}
				// Check if area exists
				if (commandPartMap.Area != null)
				{
					// Get areas
					var areas = entities.Areas;
					// Get area
					var area = commandPartMap.Area;
					// Create command input metadatum
					var commandInputMetadatum = new CommandInputMetadatum(new CommandInput(area, e => e is Area), n => areas.GetAll(n));
					// Add command input metadatum
					commandInputMetadata.Add(commandInputMetadatum);
					// Create regular expression
					var regularExpression = RegularExpressions.CreateCaptureGroup(area.ToString(), areas.RegularExpression);
					// Add regular expression
					regularExpressions.Add(regularExpression);
					// Continue
					continue;
				}
				// Check if item exists
				if (commandPartMap.Item != null)
				{
					// Get items
					var items = entities.Items;
					// Get item
					var item = commandPartMap.Item;
					// Create command input metadatum
					var commandInputMetadatum = new CommandInputMetadatum(new CommandInput(item, e => e is Item), n => items.GetAll(n));
					// Add command input metadatum
					commandInputMetadata.Add(commandInputMetadatum);
					// Create regular expression
					var regularExpression = RegularExpressions.CreateCaptureGroup(item.ToString(), items.RegularExpression);
					// Add regular expression
					regularExpressions.Add(regularExpression);
					// Continue
					continue;
				}
				// Throw error
				throw new ArgumentException("Command part map could not be handled.", nameof(commandMap));
			}
			// Set command input metadata
			_commandInputMetadata = commandInputMetadata.ToImmutable();
			// Set regular expression
			_regularExpression = new Regex(@"^" + string.Join(@" +", regularExpressions) + @"$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			// Set inputs
			Inputs = _commandInputMetadata.Select(cim => cim.CommandInput).ToImmutableArray();
			// Set fail action maps
			_actionMapsFail = commandMap.ActionMapsFail;
		}

		public override string ToString()
		{
			// Return symbol
			return Symbol.ToString();
		}

		public CommandMatch? TryGetMatch(string? input)
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
					// Run through command input metadata
					for (int i = 0; i < _commandInputMetadata.Length; i++)
					{
						var commandInputMetadatum = _commandInputMetadata[i];
						// Get command input
						var commandInput = commandInputMetadatum.CommandInput;
						// Get get entities by name
						var getEntitiesByName = commandInputMetadatum.GetEntitiesByName;
						// Get group name
						var groupName = commandInput.Symbol.ToString();
						// Get group
						var group = match.Groups[groupName];
						// Get entity name
						var entityName = new Name(group.Value);
						// Get entities
						var entities = getEntitiesByName(entityName);
						// Create part
						var part = new CommandMatchPart(commandInput, entities);
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

		public void CheckFail(Entities entities, Reactions reactions)
		{
			// Get parts
			var parts = Inputs.Select(i => entities.FirstOrDefault(e => i.IsValid(e))).ToImmutableList();
			// Check if parts exist
			if (parts.All(p => p != null))
			{
				// Create fail action
				_CreateFail(parts, reactions);
			}
		}

		public void ExecuteFail(ResultBuilder result, ImmutableList<IEntity> parts)
		{
			// Create fail action
			var actionFail = _CreateFail(parts, result.Game.Reactions);
			// Execute fail action
			actionFail(result);
		}

		private Action<ResultBuilder> _CreateFail(ImmutableList<IEntity> parts, Reactions reactions)
		{
			// Get inputs
			var inputs = Inputs;
			// Get inputs length
			var inputsLength = inputs.Length;
			// Check if parts count does not match inputs length
			if (parts.Count != inputsLength)
			{
				// Throw error
				throw new ArgumentException($"Parts count ({parts.Count}) must match input length ({inputsLength}).", nameof(parts));
			}
			// Create replacer
			Symbol replacer(Symbol symbol)
			{
				// Run through inputs
				for (int i = 0; i < inputsLength; i++)
				{
					// Check if symbol matches input
					if (inputs[i].Symbol == symbol)
					{
						// Return part symbol
						return parts[i].Symbol;
					}
				}
				// Return symbol
				return symbol;
			}
			// Return action
			return reactions.CreateAction(_actionMapsFail, replacer);
		}
	}
}

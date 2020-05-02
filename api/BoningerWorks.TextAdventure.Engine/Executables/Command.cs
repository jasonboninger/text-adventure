﻿using BoningerWorks.TextAdventure.Core.Interfaces;
using BoningerWorks.TextAdventure.Core.Static;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
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

		public Command(Areas areas, Items items, CommandMap commandMap)
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
						// Create entities in context
						var entitiesInContext = ImmutableList.CreateBuilder<IEntity>();
						// Run through entities
						for (int k = 0; k < entities.Count; k++)
						{
							var entity = entities[k];
							// Check if entity is in context
							if (entity.IsInContext(game, state))
							{
								// Add entity in context
								entitiesInContext.Add(entity);
							}
						}
						// Check if no entities in context
						if (entitiesInContext.Count == 0)
						{
							// Return no match
							return null;
						}
						// Create part
						var part = new CommandMatchPart(commandInput, entitiesInContext.ToImmutable());
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
	}
}

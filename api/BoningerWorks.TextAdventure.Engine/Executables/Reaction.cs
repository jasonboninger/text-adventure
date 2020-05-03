using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Reaction
	{
		public Command Command { get; }
		public ReactionPath Path { get; }

		private readonly ImmutableArray<Action<ResultBuilder>> _actions;

		public Reaction(Entities entities, Commands commands, ReactionMap reactionMap)
		{
			// Set command
			Command = commands.TryGet(reactionMap.CommandSymbol) 
				?? throw new InvalidOperationException($"No command with symbol ({reactionMap.CommandSymbol}) could be found.");
			// Try to create reaction
			try
			{
				// Check if reaction map has no input symbol to entity symbol mappings
				if (reactionMap.InputSymbolToEntitySymbolMappings.Count == 0)
				{
					// Check if command has more than one input
					if (Command.Inputs.Length > 1)
					{
						// Throw error
						throw new ValidationError($"Some inputs are missing.");
					}
				}
				else
				{
					// Check if reaction map does not have an input symbol for each command input
					if (Command.Inputs.Any(i => !reactionMap.InputSymbolToEntitySymbolMappings.ContainsKey(i.Symbol)))
					{
						// Throw error
						throw new ValidationError($"Some inputs are missing.");
					}
				}
				// Set path
				Path = new ReactionPath
					(
						Command,
						Command.Inputs
							.Select
								(i =>
								{
									// Try to get entity symbol for input symbol
									if (!reactionMap.InputSymbolToEntitySymbolMappings.TryGetValue(i.Symbol, out var entitySymbol))
									{
										// Set entity symbol
										entitySymbol = reactionMap.EntitySymbol;
									}
									// Try to get entity
									var entity = entities.TryGet(entitySymbol);
									// Check if entity does not exist
									if (entity == null)
									{
										// Throw error
										throw new ValidationError($"No entity with symbol ({reactionMap.EntitySymbol}) could be found.");
									}
									// Check if entity is not valid
									if (!i.IsValid(entity))
									{
										// Throw error
										throw new ValidationError($"Entity ({entity}) is not compatible with input ({i.Symbol}).");
									}
									// Create part
									var part = new ReactionPathPart(i, entity);
									// Return part
									return part;
								})
							.ToImmutableList()
					);
				// Set actions
				_actions = reactionMap.ActionMaps.SelectMany(am => Action.Create(s => s, entities, am)).ToImmutableArray();
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError($"Reaction for command ({Command}) is not valid.").ToGenericException(exception);
			}
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

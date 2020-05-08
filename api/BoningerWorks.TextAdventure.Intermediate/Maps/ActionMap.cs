using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Intermediate.Enums;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using System.Collections.Immutable;
using System.Linq;
using Action = BoningerWorks.TextAdventure.Json.Inputs.Action;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class ActionMap
	{
		public ImmutableArray<IteratorMap>? IteratorMaps { get; }
		public IfMap<ActionMap>? IfMap { get; }
		public ImmutableArray<MessageMap>? MessageMaps { get; }
		public ImmutableArray<ChangeMap>? ChangeMaps { get; }
		public ImmutableArray<TriggerMap>? TriggerMaps { get; }
		public EActionSpecial? Special { get; }

		internal ActionMap(Action? action)
		{
			// Check if action does not exist
			if (action == null)
			{
				// Throw error
				throw new ValidationError("Action cannot be null.");
			}
			// Try to create action
			try
			{
				// Create count
				var count = 0;
				// Check if iterators exists
				if (action.Iterators != null)
				{
					// Increase count
					count++;
					// Check if iterators is empty
					if (action.Iterators.Count == 0)
					{
						// Throw error
						throw new ValidationError("Iterators cannot be empty.");
					}
					// Set iterator maps
					IteratorMaps = action.Iterators.Select(i => new IteratorMap(i)).ToImmutableArray();
				}
				// Check if if exists
				if (action.If != null)
				{
					// Increase count
					count++;
					// Set if map
					IfMap = new IfMap<ActionMap>
						(
							new ConditionMap(action.If.Condition),
							action.If.ValuesTrue?.Select(a => new ActionMap(a)).ToImmutableArray(),
							action.If.ValuesFalse?.Select(a => new ActionMap(a)).ToImmutableArray()
						);
				}
				// Check if messages exists
				if (action.Messages != null)
				{
					// Increase count
					count++;
					// Check if messages is empty
					if (action.Messages.Count == 0)
					{
						// Throw error
						throw new ValidationError("Messages cannot be empty.");
					}
					// Set message maps
					MessageMaps = action.Messages.Select(m => new MessageMap(m)).ToImmutableArray();
				}
				// Check if changes exists
				if (action.Changes != null)
				{
					// Increase count
					count++;
					// Check if changes is empty
					if (action.Changes.Count == 0)
					{
						// Throw error
						throw new ValidationError("Changes cannot be empty.");
					}
					// Set change maps
					ChangeMaps = action.Changes.Select(c => new ChangeMap(c)).ToImmutableArray();
				}
				// Check if triggers exists
				if (action.Triggers != null)
				{
					// Increase count
					count++;
					// Check if triggers is empty
					if (action.Triggers.Count == 0)
					{
						// Throw error
						throw new ValidationError("Triggers cannot be empty.");
					}
					// Set trigger maps
					TriggerMaps = action.Triggers.Select(t => new TriggerMap(t)).ToImmutableArray();
				}
				// Check if special exists
				if (action.Special != null)
				{
					// Increase count
					count++;
					// Set special
					Special = action.Special switch
					{
						"END" => EActionSpecial.End,
						_ => throw new ValidationError($"Special action ({action.Special}) could not be found.")
					};
				}
				// Check if count is zero
				if (count == 0)
				{
					// Throw error
					throw new ValidationError("No action was provided.");
				}
				// Check if count is not one
				if (count != 1)
				{
					// Throw error
					throw new ValidationError($"Only one type (iterators, if, messages, changes, or triggers) is allowed at a time.");
				}
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError("Action is not valid.").ToGenericException(exception);
			}
		}
	}
}

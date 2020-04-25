using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Maps.Enums;
using BoningerWorks.TextAdventure.Maps.Errors;
using System.Collections.Immutable;
using System.Linq;
using Action = BoningerWorks.TextAdventure.Json.Inputs.Action;

namespace BoningerWorks.TextAdventure.Maps.Models
{
	public class ActionMap
	{
		public EActionMapType Type { get; }
		public IfMap<ActionMap>? IfMap { get; }
		public ImmutableArray<MessageMap>? MessageMaps { get; }
		public ImmutableArray<ChangeMap>? ChangeMaps { get; }
		public ImmutableArray<TriggerMap>? TriggerMaps { get; }

		public ActionMap(Action? action)
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
				// Check if if exists
				if (action.If != null)
				{
					// Increase count
					count++;
					// Set type
					Type = EActionMapType.If;
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
					// Set type
					Type = EActionMapType.Messages;
					// Check if messages is empty
					if (action.Messages.Count == 0)
					{
						// Throw error
						throw new ValidationError("Messages cannot be empty.");
					}
					// Set message maps
					MessageMaps = action.Messages.Select(m => new MessageMap(m)).ToImmutableArray();
				}
				// Check if change path to new value mappings exists
				if (action.ChangePathToNewValueMappings != null)
				{
					// Increase count
					count++;
					// Set type
					Type = EActionMapType.Changes;
					// Check if change path to new value mappings is empty
					if (action.ChangePathToNewValueMappings.Count == 0)
					{
						// Throw error
						throw new ValidationError("Changes cannot be empty.");
					}
					// Set change maps
					ChangeMaps = action.ChangePathToNewValueMappings.Select(kv => new ChangeMap(kv.Key, kv.Value)).ToImmutableArray();
				}
				// Check if triggers exists
				if (action.Triggers != null)
				{
					// Increase count
					count++;
					// Set type
					Type = EActionMapType.Triggers;
					// Check if triggers is empty
					if (action.Triggers.Count == 0)
					{
						// Throw error
						throw new ValidationError("Triggers cannot be empty.");
					}
					// Set trigger maps
					TriggerMaps = action.Triggers.Select(t => new TriggerMap(t)).ToImmutableArray();
				}
				// Check if count is not one
				if (count != 1)
				{
					// Throw error
					throw new ValidationError($"Exactly one action type is required, but instead has {count}.");
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

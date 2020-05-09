using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class ConditionInputMap
	{
		public Id InputId { get; }
		public ConditionMap ConditionMap { get; }

		public ConditionInputMap(ConditionArea? conditionArea)
		{
			// Check if area condition does not exist
			if (conditionArea == null)
			{
				// Throw error
				throw new ValidationError("Area condition cannot be null.");
			}
			// Set input ID
			InputId = Id.TryCreate(conditionArea.Area) ?? throw new ValidationError($"Area ID ({conditionArea.Area}) is not valid.");
			// Set condition map
			ConditionMap = new ConditionMap(conditionArea.Condition);
		}
		public ConditionInputMap(ConditionItem? conditionItem)
		{
			// Check if item condition does not exist
			if (conditionItem == null)
			{
				// Throw error
				throw new ValidationError("Item condition cannot be null.");
			}
			// Set input ID
			InputId = Id.TryCreate(conditionItem.Item) ?? throw new ValidationError($"Item ID ({conditionItem.Item}) is not valid.");
			// Set condition map
			ConditionMap = new ConditionMap(conditionItem.Condition);
		}
	}
}

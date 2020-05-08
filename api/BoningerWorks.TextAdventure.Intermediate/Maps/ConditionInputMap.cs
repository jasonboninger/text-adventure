using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class ConditionInputMap
	{
		public Symbol InputSymbol { get; }
		public ConditionMap ConditionMap { get; }

		public ConditionInputMap(ConditionArea? conditionArea)
		{
			// Check if area condition does not exist
			if (conditionArea == null)
			{
				// Throw error
				throw new ValidationError("Area condition cannot be null.");
			}
			// Set input symbol
			InputSymbol = Symbol.TryCreate(conditionArea.Area) ?? throw new ValidationError($"Area symbol ({conditionArea.Area}) is not valid.");
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
			// Set input symbol
			InputSymbol = Symbol.TryCreate(conditionItem.Item) ?? throw new ValidationError($"Item symbol ({conditionItem.Item}) is not valid.");
			// Set condition map
			ConditionMap = new ConditionMap(conditionItem.Condition);
		}
	}
}

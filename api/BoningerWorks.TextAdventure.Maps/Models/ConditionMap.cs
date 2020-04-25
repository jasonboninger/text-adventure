using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Maps.Enums;
using BoningerWorks.TextAdventure.Maps.Errors;

namespace BoningerWorks.TextAdventure.Maps.Models
{
	public class ConditionMap
	{
		public EConditionMapType Type { get; }
		public ConditionSingleMap? SingleMap { get; }
		public ConditionManyMap? ManyMap { get; }

		public ConditionMap(Condition? condition)
		{
			// Check if condition does not exist
			if (condition == null)
			{
				// Throw error
				throw new ValidationError("Condition cannot be null.");
			}
			// Check if error exists
			if (condition.Error != null)
			{
				// Throw error
				throw new ValidationError(condition.Error);
			}
			// Check if left, comparison, and right exist
			if (condition.Left != null && condition.Comparison != null && condition.Right != null)
			{
				// Check if operator or conditions exist
				if (condition.Operator != null || condition.Conditions != null)
				{
					// Throw error
					throw new ValidationError("When left, comparison, and right exist, then operator and conditions must be null.");
				}
				// Set type
				Type = EConditionMapType.Single;
				// Set single condition map
				SingleMap = new ConditionSingleMap(condition.Left, condition.Comparison, condition.Right);
				// Return
				return;
			}
			// Check if operator and conditions exist
			if (condition.Operator != null && condition.Conditions != null)
			{
				// Check if left, comparison, or right exist
				if (condition.Left != null && condition.Comparison != null && condition.Right != null)
				{
					// Throw error
					throw new ValidationError("When operator and conditions exist, then left, comparison, and right must be null.");
				}
				// Set type
				Type = EConditionMapType.Many;
				// Set many condition map
				ManyMap = new ConditionManyMap(condition.Operator, condition.Conditions);
				// Return
				return;
			}
			// Throw error
			throw new ValidationError("Condition must have either left, comparison, and right or operator and conditions.");
		}
	}
}

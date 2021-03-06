﻿using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class ConditionMap
	{
		public ConditionSingleMap? SingleMap { get; }
		public ConditionManyMap? ManyMap { get; }

		internal ConditionMap(Condition? condition)
		{
			// Check if condition does not exist
			if (condition == null)
			{
				// Throw error
				throw new ValidationError("Condition cannot be null.");
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

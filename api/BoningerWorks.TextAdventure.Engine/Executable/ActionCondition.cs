using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Intermediate.Enums;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class ActionCondition
	{
		public static Func<State, bool> Create(Func<Id, Id> replacer, Entities entities, ConditionMap conditionMap)
		{
			// Check if single
			if (conditionMap.SingleMap != null)
			{
				// Get left replace action
				var actionReplaceLeft = ActionReplace.Create(replacer, entities, conditionMap.SingleMap.Left);
				// Get right replace action
				var actionReplaceRight = ActionReplace.Create(replacer, entities, conditionMap.SingleMap.Right);
				// Get comparison
				var comparison = conditionMap.SingleMap.Comparison;
				// Return action
				return comparison switch
				{
					EConditionComparison.Is => s => actionReplaceLeft(s) == actionReplaceRight(s),
					EConditionComparison.Not => s => actionReplaceLeft(s) != actionReplaceRight(s),
					_ => throw new InvalidOperationException($"Condition comparison ({comparison}) could not be handled.")
				};
			}
			// Check if many
			if (conditionMap.ManyMap != null)
			{
				// Get condition actions
				var actionsCondition = conditionMap.ManyMap.ConditionMaps.Select(cm => Create(replacer, entities, cm));
				// Test condition actions
				_ = actionsCondition.ToList();
				// Get operator
				var @operator = conditionMap.ManyMap.Operator;
				// Create stop
				var stop = @operator switch
				{
					EConditionOperator.All => false,
					EConditionOperator.Any => true,
					_ => throw new InvalidOperationException($"Condition operator ({@operator}) could not be handled.")
				};
				// Return action
				return s =>
				{
					// Run through condition actions
					foreach (var actionCondition in actionsCondition)
					{
						// Check if stop
						if (actionCondition(s) == stop) return stop;
					}
					// Return not stop
					return !stop;
				};
			}
			// Throw error
			throw new InvalidOperationException("Condition map could not be parsed.");
		}
	}
}

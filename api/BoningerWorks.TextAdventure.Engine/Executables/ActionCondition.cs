using BoningerWorks.TextAdventure.Intermediate.Enums;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class ActionCondition
	{
		public static Func<State, bool> Create(Entities entities, ConditionMap conditionMap)
		{
			// Check if single
			if (conditionMap.SingleMap != null)
			{
				// Get left
				var left = new Replaceable(conditionMap.SingleMap.Left);
				// Get right
				var right = new Replaceable(conditionMap.SingleMap.Right);
				// Get comparison
				var comparison = conditionMap.SingleMap.Comparison;
				// Check if is
				if (comparison == EConditionComparison.Is)
				{
					// Return action
					return state => left.Replace(state) == right.Replace(state);
				}
				// Check if not
				if (comparison == EConditionComparison.Not)
				{
					// Return action
					return state => left.Replace(state) != right.Replace(state);
				}
				// Throw error
				throw new InvalidOperationException($"Condition comparison ({conditionMap.SingleMap.Comparison}) could not be handled.");
			}
			// Check if many
			if (conditionMap.ManyMap != null)
			{
				// Get condition actions
				var actionsCondition = conditionMap.ManyMap.ConditionMaps.Select(cm => Create(entities, cm)).ToImmutableArray();
				// Get operator
				var @operator = conditionMap.ManyMap.Operator;
				// Check if all
				if (@operator == EConditionOperator.All)
				{
					// Return action
					return state =>
					{
						// Run through condition actions
						for (int i = 0; i < actionsCondition.Length; i++)
						{
							// Check if not condition action
							if (!actionsCondition[i](state))
							{
								// Return not
								return false;
							}
						}
						// Return is
						return true;
					};
				}
				// Check if any
				if (@operator == EConditionOperator.Any)
				{
					// Return action
					return state =>
					{
						// Run through condition actions
						for (int i = 0; i < actionsCondition.Length; i++)
						{
							// Check if condition action
							if (actionsCondition[i](state))
							{
								// Return is
								return true;
							}
						}
						// Return not
						return false;
					};
				}
				// Throw error
				throw new InvalidOperationException($"Condition operator ({conditionMap.ManyMap.Operator}) could not be handled.");
			}
			// Throw error
			throw new InvalidOperationException("Condition map could not be parsed.");
		}
	}
}

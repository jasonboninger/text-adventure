using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Json.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Enums;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class ConditionManyMap
	{
		public EConditionOperator Operator { get; }
		public ImmutableArray<ConditionMap> ConditionMaps { get; }

		internal ConditionManyMap(string @operator, List<SFlexibleObject<Condition>> conditions)
		{
			// Set operator
			Operator = @operator switch
			{
				"ALL" => EConditionOperator.All,
				"ANY" => EConditionOperator.Any,
				_ => throw new ValidationError($"Operator ({@operator}) could not be found.")
			};
			// Set condition maps
			ConditionMaps = conditions.Select(c => new ConditionMap(c)).ToImmutableArray();
		}
	}
}

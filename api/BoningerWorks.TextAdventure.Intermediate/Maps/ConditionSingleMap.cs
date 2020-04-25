using BoningerWorks.TextAdventure.Intermediate.Enums;
using BoningerWorks.TextAdventure.Intermediate.Errors;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class ConditionSingleMap
	{
		public string Left { get; }
		public EConditionComparison Comparison { get; }
		public string Right { get; }

		internal ConditionSingleMap(string left, string comparison, string right)
		{
			// Set left
			Left = left;
			// Set comparison
			Comparison = comparison switch
			{
				"IS" => EConditionComparison.Is,
				"NOT" => EConditionComparison.Not,
				_ => throw new ValidationError($"Comparison ({comparison}) could not be found.")
			};
			// Set right
			Right = right;
		}
	}
}

using BoningerWorks.TextAdventure.Maps.Enums;
using BoningerWorks.TextAdventure.Maps.Errors;

namespace BoningerWorks.TextAdventure.Maps.Models
{
	public class ConditionSingleMap
	{
		public string Left { get; }
		public EConditionComparison Comparison { get; }
		public string Right { get; }

		public ConditionSingleMap(string left, string comparison, string right)
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

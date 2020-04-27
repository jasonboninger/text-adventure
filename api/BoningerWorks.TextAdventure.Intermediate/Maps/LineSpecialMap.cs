using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Outputs.Enums;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class LineSpecialMap
	{
		public ELineSpecialType Type { get; }

		internal LineSpecialMap(string type)
		{
			// Set type
			Type = type switch
			{
				"HORIZONTAL_RULE" => ELineSpecialType.HorizontalRule,
				"BLANK" => ELineSpecialType.Blank,
				_ => throw new ValidationError($"Special line type ({type}) could not be found."),
			};
		}
	}
}

using BoningerWorks.TextAdventure.Maps.Enums;
using BoningerWorks.TextAdventure.Maps.Errors;

namespace BoningerWorks.TextAdventure.Maps.Models
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

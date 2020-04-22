using System;

namespace BoningerWorks.TextAdventure.Engine.Maps
{
	public class LineSpecialMap
	{
		public ELineSpecialType Type { get; }

		public LineSpecialMap(string type)
		{
			// Set type
			Type = type switch
			{
				"HORIZONTAL_RULE" => ELineSpecialType.HorizontalRule,
				"BLANK" => ELineSpecialType.Blank,
				_ => throw new ArgumentException($"Special line type ({type}) could not be found."),
			};
		}
	}
}

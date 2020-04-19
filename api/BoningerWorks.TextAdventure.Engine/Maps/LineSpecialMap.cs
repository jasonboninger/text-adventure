using System;

namespace BoningerWorks.TextAdventure.Engine.Maps
{
	public class LineSpecialMap
	{
		public ELineSpecialType Type { get; }

		public LineSpecialMap(string type)
		{
			// Set type
			Type = Enum.Parse<ELineSpecialType>(type, ignoreCase: false);
		}
	}
}

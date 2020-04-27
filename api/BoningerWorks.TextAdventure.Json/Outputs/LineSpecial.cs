using BoningerWorks.TextAdventure.Json.Outputs.Enums;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class LineSpecial
	{
		public ELineSpecialType Type { get; }

		public LineSpecial(ELineSpecialType type)
		{
			// Set type
			Type = type;
		}
	}
}

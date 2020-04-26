using BoningerWorks.TextAdventure.Json.States.Enums;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class LineSpecialState
	{
		public ELineSpecialType Type { get; }

		public LineSpecialState(ELineSpecialType type)
		{
			// Set type
			Type = type;
		}
	}
}

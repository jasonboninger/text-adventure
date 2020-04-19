using BoningerWorks.TextAdventure.Engine.Maps;

namespace BoningerWorks.TextAdventure.Engine.States.Lines
{
	public class LineSpecialState
	{
		public static LineSpecialState Create(ELineSpecialType type)
		{
			// Create special line state
			var lineSpecialState = new LineSpecialState
			{
				Type = type
			};
			// Return special line state
			return lineSpecialState;
		}

		public ELineSpecialType Type { get; set; }
	}
}

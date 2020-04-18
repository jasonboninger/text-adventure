using BoningerWorks.TextAdventure.Engine.States.Messages.Lines.Enums;

namespace BoningerWorks.TextAdventure.Engine.States.Messages.Lines
{
	public class LineState
	{
		public LineInlinedState Inlined { get; set; }
		public ELineSpecial Special { get; set; }
	}
}

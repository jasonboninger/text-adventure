using BoningerWorks.TextAdventure.Engine.Executables;

namespace BoningerWorks.TextAdventure.Engine.Exceptions.Data
{
	public class AmbiguousCommandItemMatchData : AmbiguousItemMatchData
	{
		public Command Command { get; }

		public AmbiguousCommandItemMatchData(Command command, AmbiguousItemMatchData ambiguousItemMatchData) 
		: base(ambiguousItemMatchData.Name, ambiguousItemMatchData.Items)
		{
			// Set command
			Command = command;
		}
	}
}

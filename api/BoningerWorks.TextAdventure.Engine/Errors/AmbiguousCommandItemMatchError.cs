using BoningerWorks.TextAdventure.Core.Interfaces;
using BoningerWorks.TextAdventure.Engine.Executables;

namespace BoningerWorks.TextAdventure.Engine.Errors
{
	public class AmbiguousCommandItemMatchError : IError
	{
		public string Message => $"Command ({Command}) item name ({AmbiguousItemMatchError.Name}) matches more than one item.";
		public Command Command { get; }
		public AmbiguousItemMatchError AmbiguousItemMatchError { get; }

		public AmbiguousCommandItemMatchError(Command command, AmbiguousItemMatchError ambiguousItemMatchError) 
		{
			// Set command
			Command = command;
			// Set ambiguous item match error
			AmbiguousItemMatchError = ambiguousItemMatchError;
		}
	}
}

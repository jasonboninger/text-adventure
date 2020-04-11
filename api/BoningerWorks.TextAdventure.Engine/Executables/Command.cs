using BoningerWorks.TextAdventure.Engine.Utilities;
using System;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Command
	{
		public Symbol Symbol { get; }
		public CommandParser Parser { get; }

		public Command(Symbol symbol, CommandParser commandParser)
		{
			// Set symbol
			Symbol = symbol ?? throw new ArgumentException("Symbol cannot be null.", nameof(symbol));
			// Set parser
			Parser = commandParser ?? throw new ArgumentException("Command parser cannot be null.", nameof(commandParser));
		}

		public override string ToString()
		{
			// Return symbol
			return Symbol.ToString();
		}
	}
}

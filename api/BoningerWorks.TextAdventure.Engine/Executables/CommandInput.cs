using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using System;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class CommandInput
	{
		public Symbol Symbol { get; }

		private readonly Func<IEntity, bool> _isValid;

		public CommandInput(Symbol symbol, Func<IEntity, bool> isValid)
		{
			// Set symbol
			Symbol = symbol;
			// Set is valid
			_isValid = isValid;
		}

		public bool IsValid(IEntity entity)
		{
			// Return if valid
			return _isValid(entity);
		}
	}
}

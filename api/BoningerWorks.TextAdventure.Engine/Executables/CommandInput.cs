using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using System;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class CommandInput
	{
		public Id Id { get; }

		private readonly Func<IEntity, bool> _isValid;

		public CommandInput(Id id, Func<IEntity, bool> isValid)
		{
			// Set ID
			Id = id;
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

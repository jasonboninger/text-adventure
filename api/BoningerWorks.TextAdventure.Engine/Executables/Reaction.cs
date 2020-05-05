using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Reaction
	{
		public ReactionPath Path { get; }

		private readonly Action<ResultBuilder> _action;

		public Reaction
		(
			Triggers triggers,
			Entities entities, 
			Commands commands, 
			ImmutableArray<ReactionPath> paths, 
			ReactionPath path,
			ImmutableArray<ActionMap> actionMaps
		)
		{
			// Try to create reaction
			try
			{
				// Set path
				Path = path;
				// Set action
				_action = Actions.Create(triggers, entities, commands, paths, path, actionMaps);
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError($"Reaction for command ({Path.Command}) is not valid.").ToGenericException(exception);
			}
		}

		public void Execute(ResultBuilder result)
		{
			// Execute action
			_action(result);
		}

		public override string ToString()
		{
			// Return string
			return Path.ToString();
		}
	}
}

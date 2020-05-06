using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class ActionTrigger
	{
		public static Action<ResultBuilder> Create
		(
			Triggers? triggers,
			Commands commands,
			ImmutableArray<ReactionPath> reactionPaths,
			ReactionPath? reactionPath,
			TriggerMap triggerMap
		)
		{
			// Get command
			var command = commands.TryGet(triggerMap.CommandSymbol);
			// Check if command does not exist
			if (command == null)
			{
				// Throw error
				throw new ValidationError($"No command with symbol ({triggerMap.CommandSymbol}) could be found.");
			}
			// Get reaction queries
			var reactionQueries = reactionPaths
				.Where
					(rp =>
					{
						// Return if reaction path matches trigger
						return rp.Command == command
							&& rp.Parts.Count == triggerMap.InputMap.InputSymbolToEntitySymbolMappings.Count
							&& rp.Parts.All(p => triggerMap.InputMap.InputSymbolToEntitySymbolMappings.ContainsKey(p.Input.Symbol))
							&& rp.Parts.All(p => triggerMap.InputMap.InputSymbolToEntitySymbolMappings[p.Input.Symbol] == p.Entity.Symbol);
					})
				.Select
					(rp =>
					{
						// Check if triggers and reaction path do not both exist or both not exist
						if (triggers == null != (reactionPath == null))
						{
							// Throw error
							throw new ArgumentException("Triggers and reaction path must either both be null or both be not null.");
						}
						// Check if triggers exists and reaction path exists
						if (triggers != null && reactionPath != null)
						{
							// Add trigger
							triggers.Add(reactionPath, rp);
						}
						// Create reaction query
						var reactionQuery = new ReactionQuery(command, rp.Parts.Select(p => p.Entity).ToImmutableList());
						// Return reaction query
						return reactionQuery;
					})
				.ToArray();
			// Get reaction queries length
			var reactionQueriesLength = reactionQueries.Length;
			// Check if reaction queries does not exist
			if (reactionQueriesLength == 0)
			{
				// Throw error
				throw new ValidationError($"Trigger for command ({command}) does not match any reactions.");
			}
			// Return action
			return r =>
			{
				// Get game
				var game = r.Game;
				// Run through reaction queries
				for (int i = 0; i < reactionQueriesLength; i++)
				{
					var reactionQuery = reactionQueries[i];
					// Match reactions
					var reactions = game.Reactions.GetMatches(reactionQuery);
					// Get reactions length
					var reactionsLength = reactions.Length;
					// Run through reactions
					for (int k = 0; k < reactionsLength; k++)
					{
						// Execute reaction
						reactions[k].Execute(r);
					}
				}
			};
		}
	}
}

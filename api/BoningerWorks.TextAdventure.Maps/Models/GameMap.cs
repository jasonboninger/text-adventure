using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Maps.Errors;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Maps.Models
{
	public class GameMap
	{
		public ImmutableArray<CommandMap> CommandMaps { get; }
		public PlayerMap PlayerMap { get; }
		public ImmutableArray<AreaMap> AreaMaps { get; }
		public ImmutableArray<ActionMap> ActionMapsStart { get; }
		public ImmutableArray<ActionMap> ActionMapsEnd { get; }

		public GameMap(Game? game)
		{
			// Check if game does not exist
			if (game == null)
			{
				// Throw error
				throw new ValidationError("Game cannot be null.");
			}
			// Check if command symbol to command mappings does not exist
			if (game.CommandSymbolToCommandMappings == null || game.CommandSymbolToCommandMappings.Count == 0)
			{
				// Throw error
				throw new ValidationError("Command symbol to command mappings cannot be null or empty.");
			}
			// Set command maps
			CommandMaps = game.CommandSymbolToCommandMappings.Select(kv => new CommandMap(kv.Key, kv.Value)).ToImmutableArray();
			// Set player map
			PlayerMap = new PlayerMap(game.Player);
			// Check if area symbol to area mappings does not exist
			if (game.AreaSymbolToAreaMappings == null || game.AreaSymbolToAreaMappings.Count == 0)
			{
				// Throw error
				throw new ValidationError("Area symbol to area mappings cannot be null or empty.");
			}
			// Set area maps
			AreaMaps = game.AreaSymbolToAreaMappings.Select(kv => new AreaMap(kv.Key, kv.Value)).ToImmutableArray();
			// Set start action maps
			ActionMapsStart = game.ActionsStart?.Select(a => new ActionMap(a)).ToImmutableArray() ?? ImmutableArray<ActionMap>.Empty;
			// Set end action maps
			ActionMapsEnd = game.ActionsEnd?.Select(a => new ActionMap(a)).ToImmutableArray() ?? ImmutableArray<ActionMap>.Empty;
		}
	}
}

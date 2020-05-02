using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class GameMap
	{
		public static GameMap Deserialize(string json) => new GameMap(Game.Deserialize(json));

		public ImmutableArray<CommandMap> CommandMaps { get; }
		public PlayerMap PlayerMap { get; }
		public ImmutableArray<AreaMap> AreaMaps { get; }
		public ImmutableArray<ItemMap> ItemMaps { get; }
		public ImmutableArray<ReactionMap> ReactionMaps { get; }
		public ImmutableArray<ActionMap> ActionMapsStart { get; }
		public ImmutableArray<ActionMap> ActionMapsEnd { get; }

		private GameMap(Game? game)
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
			// Check if not all command symbols are unqiue
			if (CommandMaps.Select(cm => cm.CommandSymbol).Distinct().Count() != CommandMaps.Length)
			{
				// Throw error
				throw new ValidationError("Not all command symbols are unique.");
			}
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
			// Check if not all area symbols are unqiue
			if (AreaMaps.Select(am => am.AreaSymbol).Distinct().Count() != AreaMaps.Length)
			{
				// Throw error
				throw new ValidationError("Not all area symbols are unique.");
			}
			// Set item maps
			ItemMaps = Enumerable.Empty<ItemMap>() 
				.Concat(PlayerMap.ItemMaps)
				.Concat(AreaMaps.SelectMany(am => am.ItemMaps))
				.SelectMany(im => im.ItemMaps.Append(im))
				.ToImmutableArray();
			// Set reaction maps
			ReactionMaps = Enumerable.Empty<ReactionMap>()
				.Concat(PlayerMap.ReactionMaps)
				.Concat(AreaMaps.SelectMany(am => am.ReactionMaps))
				.Concat(ItemMaps.SelectMany(im => im.ReactionMaps))
				.ToImmutableArray();
			// Check if not all item symbols are unique
			if (ItemMaps.Select(im => im.ItemSymbol).Distinct().Count() != ItemMaps.Length)
			{
				// Throw error
				throw new ValidationError("Not all item symbols are unique.");
			}
			// Check if not all item names are unique
			if (ItemMaps.Select(im => im.ItemNames.Name).Distinct().Count() != ItemMaps.Length)
			{
				// Throw error
				throw new ValidationError("Not all item names are unique.");
			}
			// Set start action maps
			ActionMapsStart = game.ActionsStart?.Select(a => new ActionMap(a)).ToImmutableArray() ?? ImmutableArray<ActionMap>.Empty;
			// Set end action maps
			ActionMapsEnd = game.ActionsEnd?.Select(a => new ActionMap(a)).ToImmutableArray() ?? ImmutableArray<ActionMap>.Empty;
			// Create symbols
			var symbols = Enumerable.Empty<Symbol>()
				.Concat(CommandMaps.Select(cm => cm.CommandSymbol))
				.Append(PlayerMap.PlayerSymbol)
				.Concat(AreaMaps.Select(am => am.AreaSymbol))
				.Concat(ItemMaps.Select(im => im.ItemSymbol))
				.ToList();
			// Check if not all symbols are unique
			if (symbols.Distinct().Count() != symbols.Count)
			{
				// Throw error
				throw new ValidationError("Not all symbols are unique.");
			}
		}
	}
}

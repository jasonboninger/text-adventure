using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Static;
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
		public ImmutableArray<ActionMap> ActionMapsPrompt { get; }
		public ImmutableArray<ActionMap> ActionMapsFail { get; }
		public ImmutableArray<ActionMap> ActionMapsAreaAmbiguous { get; }
		public ImmutableArray<ActionMap> ActionMapsItemAmbiguous { get; }
		public ConditionInputMap? ConditionAreaMap { get; }
		public ConditionInputMap? ConditionItemMap { get; }
		public OptionsMap OptionsMap { get; }

		private GameMap(Game? game)
		{
			// Check if game does not exist
			if (game == null)
			{
				// Throw error
				throw new ValidationError("Game cannot be null.");
			}
			// Check if commands does not exist
			if (game.Commands == null || game.Commands.Count == 0)
			{
				// Throw error
				throw new ValidationError("Commands cannot be null or empty.");
			}
			// Set command maps
			CommandMaps = game.Commands.Select(c => new CommandMap(c)).ToImmutableArray();
			// Check if not all command IDs are unqiue
			if (CommandMaps.Select(cm => cm.CommandId).Distinct().Count() != CommandMaps.Length)
			{
				// Throw error
				throw new ValidationError("Not all command IDs are unique.");
			}
			// Set player map
			PlayerMap = new PlayerMap(game.Player);
			// Set area maps
			AreaMaps = game.Areas?.Select(a => new AreaMap(a)).ToImmutableArray() ?? ImmutableArray<AreaMap>.Empty;
			// Check if not all area IDs are unqiue
			if (AreaMaps.Select(am => am.AreaId).Distinct().Count() != AreaMaps.Length)
			{
				// Throw error
				throw new ValidationError("Not all area IDs are unique.");
			}
			// Check if not all area names are unqiue
			if (AreaMaps.Select(am => am.AreaNames.Name).Distinct().Count() != AreaMaps.Length)
			{
				// Throw error
				throw new ValidationError("Not all area names are unique.");
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
			// Check if not all item IDs are unique
			if (ItemMaps.Select(im => im.ItemId).Distinct().Count() != ItemMaps.Length)
			{
				// Throw error
				throw new ValidationError("Not all item IDs are unique.");
			}
			// Check if not all item names are unique
			if (ItemMaps.Select(im => im.ItemNames.Name).Distinct().Count() != ItemMaps.Length)
			{
				// Throw error
				throw new ValidationError("Not all item names are unique.");
			}
			// Create IDs
			var ids = Enumerable.Empty<Id>()
				.Concat(CommandMaps.Select(cm => cm.CommandId))
				.Append(PlayerMap.PlayerId)
				.Concat(AreaMaps.Select(am => am.AreaId))
				.Concat(ItemMaps.Select(im => im.ItemId))
				.ToList();
			// Check if not all IDs are unique
			if (ids.Distinct().Count() != ids.Count)
			{
				// Throw error
				throw new ValidationError("Not all IDs are unique.");
			}
			// Set start action maps
			ActionMapsStart = game.ActionsStart?.Select(a => new ActionMap(a)).ToImmutableArray() ?? ImmutableArray<ActionMap>.Empty;
			// Set end action maps
			ActionMapsEnd = game.ActionsEnd?.Select(a => new ActionMap(a)).ToImmutableArray() ?? ImmutableArray<ActionMap>.Empty;
			// Set prompt action maps
			ActionMapsPrompt = game.ActionsPrompt?.Select(a => new ActionMap(a)).ToImmutableArray() ?? Defaults.ActionMapsPrompt;
			// Set fail action maps
			ActionMapsFail = game.ActionsFail?.Select(a => new ActionMap(a)).ToImmutableArray() ?? ImmutableArray<ActionMap>.Empty;
			// Set ambiguous area action maps
			ActionMapsAreaAmbiguous = game.ActionsAreaAmbiguous?
				.Select(a => new ActionMap(a))
				.ToImmutableArray() 
				?? Defaults.ActionMapsAreaAmbiguous;
			// Set ambiguous item action maps
			ActionMapsItemAmbiguous = game.ActionsItemAmbiguous?
				.Select(a => new ActionMap(a))
				.ToImmutableArray() 
				?? Defaults.ActionMapsItemAmbiguous;
			// Set area condition map
			ConditionAreaMap = game.ConditionArea == null ? null : new ConditionInputMap(game.ConditionArea);
			// Set item condition map
			ConditionItemMap = game.ConditionItem == null ? null : new ConditionInputMap(game.ConditionItem);
			// Set options map
			OptionsMap = new OptionsMap(game.Options);
		}
	}
}

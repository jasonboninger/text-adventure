using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Json.Utilities;
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
		public ImmutableArray<ActionMap> ActionMapsFail { get; }
		public ImmutableArray<ActionMap> ActionMapsPrompt { get; }
		public ConditionInputMap? ConditionAreaMap { get; }
		public ConditionInputMap? ConditionItemMap { get; }

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
			// Check if not all command symbols are unqiue
			if (CommandMaps.Select(cm => cm.CommandSymbol).Distinct().Count() != CommandMaps.Length)
			{
				// Throw error
				throw new ValidationError("Not all command symbols are unique.");
			}
			// Set player map
			PlayerMap = new PlayerMap(game.Player);
			// Check if areas does not exist
			if (game.Areas == null || game.Areas.Count == 0)
			{
				// Throw error
				throw new ValidationError("Areas cannot be null or empty.");
			}
			// Set area maps
			AreaMaps = game.Areas.Select(a => new AreaMap(a)).ToImmutableArray();
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
			// Set fail action maps
			ActionMapsFail = game.ActionsFail?.Select(a => new ActionMap(a)).ToImmutableArray() ?? ImmutableArray<ActionMap>.Empty;
			// Set prompt action maps
			ActionMapsPrompt = game.ActionsPrompt?
				.Select(a => new ActionMap(a))
				.ToImmutableArray() 
				?? ImmutableArray.Create(new ActionMap(new Action
				{
					Messages = new OneOrManyList<SFlexibleObject<Message>>
					{
						new Message
						{
							Lines = new OneOrManyList<SFlexibleObject<Line>>
							{
								new Line
								{
									Texts = new OneOrManyList<SFlexibleObject<Text>>
									{
										new Text
										{
											Value = "What do you want to do next?"
										}
									}
								}
							}
						}
					}
				}));
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
			// Set area condition map
			ConditionAreaMap = game.ConditionArea == null ? null : new ConditionInputMap(game.ConditionArea);
			// Set item condition map
			ConditionItemMap = game.ConditionItem == null ? null : new ConditionInputMap(game.ConditionItem);
		}
	}
}

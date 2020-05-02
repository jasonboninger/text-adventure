using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class PlayerMap
	{
		public Symbol PlayerSymbol { get; }
		public Names PlayerNames { get; }
		public Symbol AreaSymbol { get; }

		internal ImmutableArray<ItemMap> ItemMaps { get; }
		internal ImmutableArray<ReactionMap> ReactionMaps { get; }

		internal PlayerMap(Player? player)
		{
			// Check if player does not exist
			if (player == null)
			{
				// Throw error
				throw new ValidationError("Player cannot be null.");
			}
			// Set player symbol
			PlayerSymbol = Symbol.TryCreate(player.Id) ?? throw new ValidationError($"Player symbol ({player.Id}) is not valid.");
			// Try to create player
			try
			{
				// Check if names does not exist
				if (player.Names == null || player.Names.Count == 0)
				{
					// Throw error
					throw new ValidationError("Names cannot be null or empty.");
				}
				// Set player names
				PlayerNames = Names.TryCreate(player.Names?.Select(n => Name.TryCreate(n) ?? throw new ValidationError($"Name ({n}) is not valid.")))
					?? throw new ValidationError("Names is not valid.");
				// Set area symbol
				AreaSymbol = Symbol.TryCreate(player.AreaSymbol) ?? throw new ValidationError($"Area symbol ({player.AreaSymbol}) is not valid.");
				// Set item maps
				ItemMaps = ItemMap.Create(player.ItemSymbolToItemMappings, PlayerSymbol);
				// Set reaction maps
				ReactionMaps = ReactionMap.Create(PlayerSymbol, player.Reactions);
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError("Player is not valid.").ToGenericException(exception);
			}
		}
	}
}

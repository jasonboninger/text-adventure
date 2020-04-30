using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class PlayerMap
	{
		public Symbol PlayerSymbol { get; }
		public Symbol AreaSymbol { get; }

		internal ImmutableArray<ItemMap> ItemMaps { get; }
		internal ImmutableArray<ReactionMap> ReactionMaps { get; }

		internal PlayerMap(Player? player)
		{
			// Set player symbol
			PlayerSymbol = new Symbol("PLAYER");
			// Check if player does not exist
			if (player == null)
			{
				// Throw error
				throw new ValidationError("Player cannot be null.");
			}
			// Try to create player
			try
			{
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

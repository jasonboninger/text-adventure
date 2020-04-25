using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Maps.Errors;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Maps.Models
{
	public class PlayerMap
	{
		public Symbol AreaSymbol { get; }
		public ImmutableArray<ItemMap> ItemMaps { get; }

		public PlayerMap(Player? player)
		{
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
				ItemMaps = player.ItemSymbolToItemMappings?
					.Select(kv => new ItemMap(kv.Key, Symbol.Player, kv.Value))
					.ToImmutableArray()
					?? ImmutableArray<ItemMap>.Empty;
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError("Player is not valid.").ToGenericException(exception);
			}
		}
	}
}

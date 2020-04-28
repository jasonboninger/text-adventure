using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Player
	{
		public Symbol Symbol { get; }
		public Symbol Area { get; }

		public Player(Areas areas, PlayerMap playerMap)
		{
			// Set symbol
			Symbol = playerMap.PlayerSymbol ?? throw new ValidationError("Player symbol cannot be null.");
			// Check if area does not exist
			if (playerMap.AreaSymbol == null)
			{
				// Throw error
				throw new ValidationError("Area symbol for player cannot be null.");
			}
			// Check if area does not exist
			if (!areas.Contains(playerMap.AreaSymbol))
			{
				// Throw error
				throw new ValidationError($"Area symbol ({playerMap.AreaSymbol}) for player could not be found.");
			}
			// Set area
			Area = playerMap.AreaSymbol;
		}
	}
}

using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Player
	{
		public Symbol Symbol { get; }
		public Symbol Area { get; }

		public Player(PlayerMap playerMap)
		{
			// Set symbol
			Symbol = playerMap.PlayerSymbol ?? throw new ValidationError("Player symbol cannot be null.");
			// Set area
			Area = playerMap.AreaSymbol ?? throw new ValidationError("Player area symbol cannot be null.");
		}
	}
}

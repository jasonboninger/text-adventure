using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Player : IEntity
	{
		private static readonly Symbol _datumArea = new Symbol("AREA");

		public Symbol Symbol { get; }
		public Symbol Area { get; }

		private readonly Areas _areas;

		public Player(Areas areas, PlayerMap playerMap)
		{
			// Set areas
			_areas = areas;
			// Set symbol
			Symbol = playerMap.PlayerSymbol ?? throw new ValidationError("Player symbol cannot be null.");
			// Set area
			Area = playerMap.AreaSymbol ?? throw new ValidationError("Player area cannot be null.");
			// Check if area does not exist
			if (!areas.Contains(playerMap.AreaSymbol))
			{
				// Throw error
				throw new ValidationError($"Player area ({playerMap.AreaSymbol}) could not be found.");
			}
		}

		Entity IEntity.Create()
		{
			// Return entity
			return new Entity(ImmutableDictionary.CreateRange(new KeyValuePair<Symbol, Symbol>[] 
			{
				KeyValuePair.Create(_datumArea, Area)
			}));
		}

		bool IEntity.HasData(Symbol symbol)
		{
			// Return if data
			return symbol == _datumArea;
		}

		void IEntity.EnsureValidData(Symbol symbol, Symbol value)
		{
			// Check if area
			if (symbol == _datumArea)
			{
				// Check if area does not exist
				if (!_areas.Contains(value))
				{
					// Throw error
					throw new ValidationError($"Player data ({symbol}) value ({value}) must be set to an area.");
				}
				// Return
				return;
			}
			// Throw error
			throw new ValidationError($"Player data ({symbol}) could not be found.");
		}
	}
}

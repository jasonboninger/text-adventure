using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Item : IEntity
	{
		private static readonly Symbol _datumActive = new Symbol("ACTIVE");
		private static readonly Symbol _datumLocation = new Symbol("LOCATION");

		public Symbol Symbol { get; }
		public Names Names { get; }
		public Name Name { get; }
		public string RegularExpression { get; }

		private readonly Player _player;
		private readonly Areas _areas;
		public readonly Symbol _location;
		public readonly bool? _active;

		public Item(Player player, Areas areas, ItemMap itemMap)
		{
			// Set player
			_player = player;
			// Set areas
			_areas = areas;
			// Set symbol
			Symbol = itemMap.ItemSymbol;
			// Set location
			_location = itemMap.LocationSymbol ?? throw new ValidationError("Item location cannot be null.");
			// Check if location does not exist
			if (_location != player.Symbol && !areas.Contains(_location))
			{
				// Throw error
				throw new ValidationError($"Item location ({_location}) could not be found.");
			}
			// Set names
			Names = itemMap.ItemNames;
			// Set name
			Name = itemMap.ItemName;
			// Set active
			_active = itemMap.Active;
			// Set regular expression
			RegularExpression = Names.RegularExpression;
		}

		public override string ToString()
		{
			// Return string
			return Symbol.ToString();
		}

		public Symbol GetLocation(State state)
		{
			// Return location
			return state.Entities[Symbol].Data[_datumLocation];
		}

		public Symbol GetActive(State state)
		{
			// Return active
			return state.Entities[Symbol].Data[_datumActive];
		}

		Entity IEntity.Create()
		{
			// Return entity
			return new Entity(ImmutableDictionary.CreateRange(new KeyValuePair<Symbol, Symbol>[]
			{
				KeyValuePair.Create(_datumActive, _active == false ? Symbol.False : Symbol.True),
				KeyValuePair.Create(_datumLocation, _location)
			}));
		}

		bool IEntity.HasData(Symbol symbol)
		{
			// Return if data
			return symbol == _datumActive || symbol == _datumLocation;
		}

		void IEntity.EnsureValidData(Symbol symbol, Symbol value)
		{
			// Check if active
			if (symbol == _datumActive)
			{
				// Check if not true or false
				if (value != Symbol.True && value != Symbol.False)
				{
					// Throw error
					throw new ValidationError($"Item data ({symbol}) value ({value}) must be set to {Symbol.True} or {Symbol.False}.");
				}
				// Return
				return;
			}
			// Check if location
			if (symbol == _datumLocation)
			{
				// Check if not player and area does not exist
				if (value != _player.Symbol && !_areas.Contains(value))
				{
					// Throw error
					throw new ValidationError($"Item data ({symbol}) value ({value}) must be set to {_player.Symbol} or an area.");
				}
				// Return
				return;
			}
			// Throw error
			throw new ValidationError($"Item data ({symbol}) could not be found.");
		}

		bool IEntity.IsInContext(Game game, State state)
		{
			// Get location
			var location = GetLocation(state);
			// Return if active and on player or in player area
			return GetActive(state) == Symbol.True && (location == game.Player.Symbol || location == game.Player.GetArea(state));
		}
	}
}

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
		public Symbol Location { get; }
		public Names Names { get; }
		public Name Name { get; }
		public bool? Active { get; }
		public string RegularExpression { get; }

		private readonly Player _player;
		private readonly Areas _areas;

		public Item(Player player, Areas areas, ItemMap itemMap)
		{
			// Set player
			_player = player;
			// Set areas
			_areas = areas;
			// Set symbol
			Symbol = itemMap.ItemSymbol;
			// Set location
			Location = itemMap.LocationSymbol ?? throw new ValidationError("Item location cannot be null.");
			// Check if location does not exist
			if (Location != player.Symbol && !areas.Contains(Location))
			{
				// Throw error
				throw new ValidationError($"Item location ({Location}) could not be found.");
			}
			// Set names
			Names = itemMap.ItemNames;
			// Set name
			Name = itemMap.ItemName;
			// Set active
			Active = itemMap.Active;
			// Set regular expression
			RegularExpression = Names.RegularExpression;
		}

		public override string ToString()
		{
			// Return string
			return Symbol.ToString();
		}

		Entity IEntity.Create()
		{
			// Return entity
			return new Entity(ImmutableDictionary.CreateRange(new KeyValuePair<Symbol, Symbol>[]
			{
				KeyValuePair.Create(_datumActive, Active == false ? Symbol.False : Symbol.True),
				KeyValuePair.Create(_datumLocation, Location)
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
	}
}

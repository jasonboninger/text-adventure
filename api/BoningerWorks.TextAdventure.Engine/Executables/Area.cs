using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Area : IEntity
	{
		public Symbol Symbol { get; }
		public Names Names { get; }
		public Name Name { get; }
		public string RegularExpression { get; }

		public Area(AreaMap areaMap)
		{
			// Set symbol
			Symbol = areaMap.AreaSymbol;
			// Set names
			Names = areaMap.AreaNames;
			// Set name
			Name = areaMap.AreaName;
			// Set regular expression
			RegularExpression = Names.RegularExpression;
		}

		Entity IEntity.Create()
		{
			// Return entity
			return new Entity(null);
		}

		bool IEntity.HasData(Symbol symbol)
		{
			// Return if data
			return false;
		}

		void IEntity.EnsureValidData(Symbol symbol, Symbol value)
		{
			// Throw error
			throw new ValidationError($"Area data ({symbol}) could not be found.");
		}

		bool IEntity.IsInContext(Game game, State state)
		{
			// Return if player is in area
			return game.Player.GetArea(state) == Symbol;
		}
	}
}

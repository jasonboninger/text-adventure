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

		public Area(AreaMap areaMap)
		{
			// Set symbol
			Symbol = areaMap.AreaSymbol ?? throw new ValidationError("Area symbol cannot be null.");
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
	}
}

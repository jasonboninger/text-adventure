using BoningerWorks.TextAdventure.Core.Interfaces;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Executables;
using BoningerWorks.TextAdventure.Json.Outputs;

namespace BoningerWorks.TextAdventure.Engine.Interfaces
{
	public interface IEntity : INamed
	{
		Entity Create();

		bool HasData(Symbol symbol);

		void EnsureValidData(Symbol symbol, Symbol value);

		bool IsInContext(Game game, State state);
	}
}

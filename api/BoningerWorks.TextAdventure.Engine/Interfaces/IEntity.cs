using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Json.Outputs;

namespace BoningerWorks.TextAdventure.Engine.Interfaces
{
	public interface IEntity
	{
		Symbol Symbol { get; }

		Entity Create();

		bool HasData(Symbol symbol);

		void EnsureValidData(Symbol symbol, Symbol value);
	}
}

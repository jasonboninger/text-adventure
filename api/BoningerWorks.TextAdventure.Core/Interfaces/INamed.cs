using BoningerWorks.TextAdventure.Core.Utilities;

namespace BoningerWorks.TextAdventure.Core.Interfaces
{
	public interface INamed : IId
	{
		public Names Names { get; }
	}
}

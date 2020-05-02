using BoningerWorks.TextAdventure.Core.Utilities;

namespace BoningerWorks.TextAdventure.Core.Interfaces
{
	public interface INamed : IIdentifiable
	{
		public Names Names { get; }
	}
}

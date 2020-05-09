using BoningerWorks.TextAdventure.Core.Interfaces;
using BoningerWorks.TextAdventure.Core.Utilities;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Interfaces
{
	public interface IEntity : INamed
	{
		ImmutableDictionary<Id, string> Metadata { get; }
	}
}

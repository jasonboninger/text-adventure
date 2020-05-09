using BoningerWorks.TextAdventure.Core.Interfaces;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Comparers
{
	public class IdentifiableEqualityComparer<TIdentifiable> : EqualityComparer<TIdentifiable>
	where TIdentifiable : class, IIdentifiable
	{
		public static IdentifiableEqualityComparer<TIdentifiable> Instance { get; } = new IdentifiableEqualityComparer<TIdentifiable>();

		public override bool Equals(TIdentifiable? x, TIdentifiable? y) => x == null ? y == null : (y != null && x.Id == y.Id);

		public override int GetHashCode(TIdentifiable? obj) => obj == null ? 0 : obj.Id.GetHashCode();

		private IdentifiableEqualityComparer() { }
	}
}

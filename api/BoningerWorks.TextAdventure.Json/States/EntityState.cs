using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Json.States.Errors;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class EntityState<TData> : EntityState
	where TData : class
	{
		public TData Data { get; }

		public EntityState(Symbol id, TData data, ImmutableDictionary<string, string>? customData) : base(id, customData)
		{
			// Set data
			Data = data ?? throw GenericException.Create(new StateInvalidError("Data cannot be null."));
		}
	}
	public abstract class EntityState
	{
		public Symbol Id { get; }
		public ImmutableDictionary<string, string> CustomData { get; }
		
		protected EntityState(Symbol id, ImmutableDictionary<string, string>? customData)
		{
			// Set ID
			Id = id ?? throw GenericException.Create(new StateInvalidError("ID cannot be null."));
			// Set custom data
			CustomData = customData ?? ImmutableDictionary<string, string>.Empty;
		}
	}
}

namespace BoningerWorks.TextAdventure.Json.Utilities
{
	public struct SFlexibleObject<TValue>
	where TValue : class
	{
		public static implicit operator SFlexibleObject<TValue>(TValue? value) => new SFlexibleObject<TValue> { Value = value };
		public static implicit operator TValue?(SFlexibleObject<TValue> flexibleObject) => flexibleObject.Value;
		
		public static bool operator ==(SFlexibleObject<TValue> left, SFlexibleObject<TValue> right) => left.Equals(right);
		public static bool operator !=(SFlexibleObject<TValue> left, SFlexibleObject<TValue> right) => !(left == right);
		
		public TValue? Value { get; set; }

		public override bool Equals(object? obj) => obj is SFlexibleObject<TValue> flexibleObject
			? Equals(flexibleObject.Value, Value)
			: obj is TValue value && Equals(value, Value);

		public override int GetHashCode() => Value == null ? 0 : Value.GetHashCode();
	}
}

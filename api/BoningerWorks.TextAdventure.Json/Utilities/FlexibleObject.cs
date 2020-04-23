namespace BoningerWorks.TextAdventure.Json.Utilities
{
	public struct FlexibleObject<TValue>
	where TValue : class
	{
		public static implicit operator FlexibleObject<TValue>(TValue? value) => new FlexibleObject<TValue> { Value = value };
		public static implicit operator TValue?(FlexibleObject<TValue> flexibleObject) => flexibleObject.Value;
		
		public static bool operator ==(FlexibleObject<TValue> left, FlexibleObject<TValue> right) => left.Equals(right);
		public static bool operator !=(FlexibleObject<TValue> left, FlexibleObject<TValue> right) => !(left == right);
		
		public TValue? Value { get; set; }

		public override bool Equals(object? obj) => obj is FlexibleObject<TValue> flexibleObject
			? Equals(flexibleObject.Value, Value)
			: obj is TValue value && Equals(value, Value);

		public override int GetHashCode() => Value == null ? 0 : Value.GetHashCode();
	}
}

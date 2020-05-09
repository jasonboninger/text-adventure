using System;
using System.Text.RegularExpressions;

namespace BoningerWorks.TextAdventure.Core.Utilities
{
	public sealed class Id : IEquatable<Id>, IComparable<Id>
	{
		public static Id True { get; }
		public static Id False { get; }

		public static bool operator ==(Id? left, Id? right) => Equals(left, null) ? Equals(right, null) : left.Equals(right);
		public static bool operator !=(Id? left, Id? right) => !(left == right);
		public static bool operator <(Id left, Id right) => left is null ? right is object : left.CompareTo(right) < 0;
		public static bool operator <=(Id left, Id right) => left is null || left.CompareTo(right) <= 0;
		public static bool operator >(Id left, Id right) => left is object && left.CompareTo(right) > 0;
		public static bool operator >=(Id left, Id right) => left is null ? right is null : left.CompareTo(right) >= 0;

		private static readonly Regex _regularExpressionValid = new Regex(@"^[A-Z0-9_]+$", RegexOptions.Singleline);

		static Id()
		{
			// Set true
			True = new Id("TRUE");
			// Set false
			False = new Id("FALSE");
		}

		public static Id? TryCreate(string? @string)
		{
			// Create try
			var @try = true;
			// Try to create ID
			try
			{
				// Return ID
				return new Id(@string!);
			}
			catch
			{
				// Check if try
				if (@try)
				{
					// Return no ID
					return null;
				}
				// Throw error
				throw;
			}
		}

		private readonly string _value;
		private readonly int _hashCode;

		public Id(string id)
		{
			// Check if ID does not exist
			if (string.IsNullOrWhiteSpace(id))
			{
				// Throw error
				throw new ArgumentException("ID cannot be null, empty, or whitespace.", nameof(id));
			}
			// Check if not valid ID
			if (!_regularExpressionValid.IsMatch(id))
			{
				// Throw error
				throw new ArgumentException($"ID ({id}) can only contain underscores, numbers, and capital letters.", nameof(id));
			}
			// Set value
			_value = id;
			// Set hash code
			_hashCode = _value.GetHashCode();
		}

		public override string ToString() => _value;

		public override int GetHashCode() => _hashCode;

		public override bool Equals(object? obj) => obj is Id id && Equals(id);
		public bool Equals(Id? other) => !Equals(other, null) && other._value.Equals(_value);

		public int CompareTo(Id? other) => _value.CompareTo(other?._value);
	}
}

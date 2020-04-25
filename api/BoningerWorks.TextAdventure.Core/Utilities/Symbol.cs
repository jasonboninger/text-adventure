using BoningerWorks.TextAdventure.Core.Extensions;
using System;
using System.Text.RegularExpressions;

namespace BoningerWorks.TextAdventure.Core.Utilities
{
	public sealed class Symbol : IEquatable<Symbol>
	{
		public static bool operator ==(Symbol? left, Symbol? right) => Equals(left, null) ? Equals(right, null) : left.Equals(right);
		public static bool operator !=(Symbol? left, Symbol? right) => !(left == right);

		private static readonly Regex _regularExpressionValid = new Regex(@"^[A-Z0-9_]+$", RegexOptions.Singleline);

		public static Symbol? TryCreate(string? @string)
		{
			// Check if string exists and exception does not exist
			if (@string != null && _GetException(@string) == null)
			{
				// Return symbol
				return new Symbol(@string);
			}
			// Return no symbol
			return null;
		}

		private static Exception? _GetException(string? symbol)
		{
			// Check if symbol does not exist
			if (string.IsNullOrWhiteSpace(symbol))
			{
				// Return exception
				return new ArgumentException("Symbol cannot be null, empty, or whitespace.", nameof(symbol));
			}
			// Check if not valid symbol
			if (!_regularExpressionValid.IsMatch(symbol))
			{
				// Return exception
				return new ArgumentException($"Symbol ({symbol}) can only contain underscores, numbers, and capital letters.", nameof(symbol));
			}
			// Return no exception
			return null;
		}

		private readonly string _value;
		private readonly int _hashCode;

		public Symbol(string symbol)
		{
			// Throw error if exception exists
			_GetException(symbol).ThrowIfExists();
			// Set value
			_value = symbol;
			// Set hash code
			_hashCode = _value.GetHashCode();
		}

		public override string ToString() => _value;

		public override int GetHashCode() => _hashCode;

		public override bool Equals(object? obj) => obj is Symbol symbol && Equals(symbol);
		public bool Equals(Symbol? other) => !Equals(other, null) && other._value.Equals(_value);
	}
}

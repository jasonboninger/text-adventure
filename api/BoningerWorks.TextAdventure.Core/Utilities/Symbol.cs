﻿using BoningerWorks.TextAdventure.Core.Extensions;
using System;
using System.Text.RegularExpressions;

namespace BoningerWorks.TextAdventure.Core.Utilities
{
	public sealed class Symbol : IEquatable<Symbol>
	{
		public static bool operator ==(Symbol? left, Symbol? right) => Equals(left, null) ? Equals(right, null) : left.Equals(right);
		public static bool operator !=(Symbol? left, Symbol? right) => !(left == right);

		public static Symbol Global { get; }
		public static Symbol Player { get; }

		private static readonly Regex _regularExpressionValid = new Regex(@"^[A-Z0-9_]+$", RegexOptions.Singleline);

		static Symbol()
		{
			// Create global
			Global = new Symbol("GAME");
			// Create player
			Player = new Symbol("PLAYER");
		}

		public static bool TryCreate(string? @string, out Symbol symbol)
		{
			// Check if exception exists
			if (_GetException(@string) != null)
			{
				// Set symbol
				symbol = null!;
				// Return failed
				return false;
			}
			// Set symbol
			symbol = new Symbol(@string!);
			// Return succeeded
			return true;
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

﻿using System;
using System.Text.RegularExpressions;

namespace BoningerWorks.TextAdventure.Core.Utilities
{
	public sealed class Symbol : IEquatable<Symbol>
	{
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

		public static bool operator ==(Symbol left, Symbol right) => Equals(left, null) ? Equals(right, null) : left.Equals(right);
		public static bool operator !=(Symbol left, Symbol right) => !(left == right);

		private readonly string _value;
		private readonly int _hashCode;

		public Symbol(string symbol)
		{
			// Check if symbol does not exist
			if (string.IsNullOrWhiteSpace(symbol))
			{
				// Throw error
				throw new ArgumentException("Symbol cannot be empty or whitespace.", nameof(symbol));
			}
			// Check if not valid symbol
			if (!_regularExpressionValid.IsMatch(symbol))
			{
				// Throw error
				throw new ArgumentException($"Symbol ({symbol}) can only contain underscores, numbers, and capital letters.", nameof(symbol));
			}
			// Set value
			_value = symbol;
			// Set hash code
			_hashCode = _value.GetHashCode();
		}

		public override string ToString() => _value;

		public override int GetHashCode() => _hashCode;

		public override bool Equals(object obj) => obj is Symbol symbol && Equals(symbol);
		public bool Equals(Symbol other) => !Equals(other, null) && other._value.Equals(_value);
	}
}
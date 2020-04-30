using System;
using System.Text.RegularExpressions;

namespace BoningerWorks.TextAdventure.Core.Utilities
{
	public sealed class Name : IEquatable<Name>
	{
		public static bool operator ==(Name? left, Name? right) => Equals(left, null) ? Equals(right, null) : left.Equals(right);
		public static bool operator !=(Name? left, Name? right) => !(left == right);

		private static readonly Regex _regularExpressionSpaces = new Regex(@" {2,}", RegexOptions.Singleline);
		private static readonly Regex _regularExpressionValid = new Regex(@"^[a-zA-Z0-9 ]*[a-zA-Z0-9]+[a-zA-Z0-9 ]*$", RegexOptions.Singleline);

		public static Name? TryCreate(string? @string)
		{
			// Create try
			var @try = true;
			// Try to create name
			try
			{
				// Return name
				return new Name(@string!);
			}
			catch
			{
				// Check if try
				if (@try)
				{
					// Return no name
					return null;
				}
				// Throw error
				throw;
			}
		}

		public string RegularExpression { get; }

		private readonly string _raw;
		private readonly string _value;
		private readonly int _hashCode;

		public Name(string name)
		{
			// Check if name does not exist
			if (string.IsNullOrWhiteSpace(name))
			{
				// Throw error
				throw new ArgumentException("Name cannot be null, empty, or whitespace.", nameof(name));
			}
			// Check if not valid name
			if (!_regularExpressionValid.IsMatch(name))
			{
				// Throw error
				throw new ArgumentException($"Name ({name}) can only contain spaces and alphanumeric characters.", nameof(name));
			}
			// Set raw
			_raw = name;
			// Trim name
			name = name.Trim();
			// Replace spaces in name
			name = _regularExpressionSpaces.Replace(name, " ");
			// Convert name to uppercase
			name = name.ToUpperInvariant();
			// Set value
			_value = name;
			// Set hash code
			_hashCode = _value.GetHashCode();
			// Set regular expression
			RegularExpression = _CreateRegularExpression(_value);
		}

		public override string ToString() => _raw;

		public override int GetHashCode() => _hashCode;

		public override bool Equals(object? obj) => obj is Name name && Equals(name);
		public bool Equals(Name? other) => !Equals(other, null) && other._value.Equals(_value);

		private static string _CreateRegularExpression(string value)
		{
			// Create regular expression
			var regularExpression = @" *" + value.Replace(" ", @" +") + @" *";
			// Return regular expression
			return regularExpression;
		}
	}
}

using BoningerWorks.TextAdventure.Core.Extensions;
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

		public static bool TryCreate(string? @string, out Name name)
		{
			// Check if exception exists
			if (_GetException(@string) != null)
			{
				// Set name
				name = null!;
				// Return failed
				return false;
			}
			// Set name
			name = new Name(@string!);
			// Return succeeded
			return true;
		}

		private static Exception? _GetException(string? name)
		{
			// Check if name does not exist
			if (string.IsNullOrWhiteSpace(name))
			{
				// Return exception
				return new ArgumentException("Name cannot be null, empty, or whitespace.", nameof(name));
			}
			// Check if not valid name
			if (!_regularExpressionValid.IsMatch(name))
			{
				// Return exception
				return new ArgumentException($"Name ({name}) can only contain spaces and alphanumeric characters.", nameof(name));
			}
			// Return no exception
			return null;
		}

		public string RegularExpression { get; }

		private readonly string _raw;
		private readonly string _value;
		private readonly int _hashCode;

		public Name(string name)
		{
			// Throw error if exception exists
			_GetException(name).ThrowIfExists();
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

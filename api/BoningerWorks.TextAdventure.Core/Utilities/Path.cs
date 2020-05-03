using System;

namespace BoningerWorks.TextAdventure.Core.Utilities
{
	public class Path
	{
		private const char _SEPARATOR_BUILT_IN = '>';
		private const char _SEPARATOR_CUSTOM = '.';

		public static Path? TryCreate(string? target, string? datum, bool custom)
		{
			// Create try
			var @try = true;
			// Try to create path
			try
			{
				// Return path
				return new Path(target!, datum!, custom);
			}
			catch
			{
				// Check if try
				if (@try)
				{
					// Return no path
					return null;
				}
				// Throw error
				throw;
			}
		}
		public static Path? TryCreate(string? @string)
		{
			// Create try
			var @try = true;
			// Try to create path
			try
			{
				// Return path
				return new Path(@string!);
			}
			catch
			{
				// Check if try
				if (@try)
				{
					// Return no path
					return null;
				}
				// Throw error
				throw;
			}
		}

		public Symbol Target { get; }
		public Symbol Datum { get; }
		public bool Custom { get; }

		internal Path(string target, string datum, bool custom) : this(target + (custom ? _SEPARATOR_CUSTOM : _SEPARATOR_BUILT_IN) + datum) { }
		internal Path(string path)
		{
			// Check if path does not exist
			if (string.IsNullOrWhiteSpace(path))
			{
				// Throw error
				throw new ArgumentException("Path cannot be null, empty, or whitespace.", nameof(path));
			}
			// Create built-in separator
			var separator = _SEPARATOR_BUILT_IN;
			// Check if path contains custom separator
			if (path.Contains(_SEPARATOR_CUSTOM))
			{
				// Set custom separator
				separator = _SEPARATOR_CUSTOM;
				// Set custom
				Custom = true;
			}
			// Get parts
			var parts = path.Split(separator);
			// Check if not exactly two parts
			if (parts.Length != 2)
			{
				// Throw error
				throw new ArgumentException($"Path ({path}) must have exactly two parts, but instead has {parts.Length}.", nameof(path));
			}
			// Set target
			Target = new Symbol(parts[0]);
			// Set datum
			Datum = new Symbol(parts[1]);
		}
	}
}

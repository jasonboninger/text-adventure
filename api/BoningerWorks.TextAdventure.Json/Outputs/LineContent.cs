using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class LineContent
	{
		public ImmutableList<Text> Texts { get; }

		public LineContent(ImmutableList<Text> texts)
		{
			// Set texts
			Texts = texts ?? throw new ArgumentException("Texts cannot be null.", nameof(texts));
		}
	}
}

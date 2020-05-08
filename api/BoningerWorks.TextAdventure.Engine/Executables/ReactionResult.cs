using BoningerWorks.TextAdventure.Engine.Enums;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ReactionResult
	{
		public static ReactionResult OutOfContext { get; } = new ReactionResult(EReactionOutcome.OutOfContext, ImmutableArray<Reaction>.Empty);
		public static ReactionResult Nothing { get; } = new ReactionResult(EReactionOutcome.Nothing, ImmutableArray<Reaction>.Empty);

		public static ReactionResult Success(ImmutableArray<Reaction> reactions)
		{
			// Check if reactions does not exist
			if (reactions.Length == 0)
			{
				// Throw error
				throw new ArgumentException("Reactions cannot be empty.");
			}
			// Return reaction result
			return new ReactionResult(EReactionOutcome.Success, reactions);
		}

		public EReactionOutcome Outcome { get; }
		public ImmutableArray<Reaction> Reactions { get; }

		private ReactionResult(EReactionOutcome outcome, ImmutableArray<Reaction> reactions)
		{
			// Set outcome
			Outcome = outcome;
			// Set reactions
			Reactions = reactions;
		}
	}
}

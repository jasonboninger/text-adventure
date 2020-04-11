namespace BoningerWorks.TextAdventure.Engine.Static
{
	public static class RegularExpressions
	{
		public static string CreateCaptureGroup(string name, string pattern)
		{
			// Return capture group
			return @"(?<" + name + ">" + pattern + @")";
		}

		public static string CreateNonCapturingGroup(string pattern)
		{
			// Return non-capturing group
			return @"(?:" + pattern + @")";
		}
	}
}

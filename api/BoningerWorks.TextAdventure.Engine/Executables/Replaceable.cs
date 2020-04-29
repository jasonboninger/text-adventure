using BoningerWorks.TextAdventure.Json.Outputs;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Replaceable
	{
		private readonly string _value;

		public Replaceable(string value)
		{
			// Set value
			_value = value;
		}

		public string Replace(State state)
		{
			// Return value
			return _value;
		}
	}
}

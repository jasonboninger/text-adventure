namespace BoningerWorks.TextAdventure.Engine.Blueprints.Messages
{
	public class MessageBlueprint
	{
		public MessageInlinedBlueprint Inlined { get; set; }
		public MessageTemplatedBlueprint Templated { get; set; }
		public string Input { get; set; }
	}
}

using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Intermediate.Static
{
	public static class Defaults
	{
		public static ImmutableArray<ActionMap> ActionMapsPrompt { get; } = ImmutableArray.Create(new ActionMap(new Action
		{
			Messages = new OneOrManyList<SFlexibleObject<Message>>
			{
				new Message
				{
					Lines = new OneOrManyList<SFlexibleObject<Line>>
					{
						new Line
						{
							Texts = new OneOrManyList<SFlexibleObject<Text>>
							{
								new Text
								{
									Value = "What do you want to do next?"
								}
							}
						}
					}
				}
			}
		}));

		public static ImmutableArray<ActionMap> ActionMapsAreaAmbiguous { get; } = ImmutableArray.Create(new ActionMap(new Action
		{
			Messages = new OneOrManyList<SFlexibleObject<Message>>
			{
				new Message
				{
					Lines = new OneOrManyList<SFlexibleObject<Line>>
					{
						new Line
						{
							Texts = new OneOrManyList<SFlexibleObject<Text>>
							{
								new Text
								{
									Value = "It's not clear what you meant. Did you mean one of the following?"
								}
							}
						},
						new Line
						{
							Special = "BLANK"
						},
						new Line
						{
							Iterators = new OneOrManyList<Iterator<SFlexibleObject<Line>>?>
							{
								new Iterator<SFlexibleObject<Line>>
								{
									Area = "AREA",
									ValuesProcessor = new OneOrManyList<SFlexibleObject<Line>>
									{
										new Line
										{
											If = new If<SFlexibleObject<Line>>
											{
												Condition = new Condition
												{
													Left = "${AREA>AMBIGUOUS}",
													Comparison = "IS",
													Right = "TRUE"
												},
												ValuesTrue = new OneOrManyList<SFlexibleObject<Line>>
												{
													new Line
													{
														Texts = new OneOrManyList<SFlexibleObject<Text>>
														{
															new Text
															{
																Value = "${AREA>NAME}"
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}));

		public static ImmutableArray<ActionMap> ActionMapsItemAmbiguous { get; } = ImmutableArray.Create(new ActionMap(new Action
		{
			Messages = new OneOrManyList<SFlexibleObject<Message>>
			{
				new Message
				{
					Lines = new OneOrManyList<SFlexibleObject<Line>>
					{
						new Line
						{
							Texts = new OneOrManyList<SFlexibleObject<Text>>
							{
								new Text
								{
									Value = "It's not clear what you meant. Did you mean one of the following?"
								}
							}
						},
						new Line
						{
							Special = "BLANK"
						},
						new Line
						{
							Iterators = new OneOrManyList<Iterator<SFlexibleObject<Line>>?>
							{
								new Iterator<SFlexibleObject<Line>>
								{
									Item = "ITEM",
									ValuesProcessor = new OneOrManyList<SFlexibleObject<Line>>
									{
										new Line
										{
											If = new If<SFlexibleObject<Line>>
											{
												Condition = new Condition
												{
													Left = "${ITEM>AMBIGUOUS}",
													Comparison = "IS",
													Right = "TRUE"
												},
												ValuesTrue = new OneOrManyList<SFlexibleObject<Line>>
												{
													new Line
													{
														Texts = new OneOrManyList<SFlexibleObject<Text>>
														{
															new Text
															{
																Value = "${ITEM>NAME}"
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}));
	}
}

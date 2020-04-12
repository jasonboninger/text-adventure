namespace BoningerWorks.TextAdventure.Engine.Generated
{
	public static class CrazyEx
	{
		public const string JSON = 
@"{
	""templates"": {
		""commands"": {
			""INSPECT"": {
				""template"": [
					""COMMAND"",
					""ITEM""
				],
				""words"": {
					""COMMAND"": [
						""Inspect"",
						""Ins"",
						""Look at"",
						""Lookat"",
						""Look"",
						""Observe"",
						""Check""
					]
				},
				""items"": [
					""ITEM""
				]
			},
			""SURVEY"": {
				""template"": [
					""COMMAND""
				],
				""words"": {
					""COMMAND"": [
						""Survey"",
						""Look around"",
						""Lookaround"",
						""Look"",
						""Observe""
					]
				}
			},
			""USE"": {
				""template"": [
					""COMMAND"",
					""ITEM""
				],
				""words"": {
					""COMMAND"": [
						""Use""
					]
				},
				""items"": [
					""ITEM""
				]
			},
			""USE_ON"": {
				""template"": [
					""COMMAND_START"",
					""ITEM_TO_USE"",
					""COMMAND_JOIN"",
					""ITEM_TO_USE_ON""
				],
				""words"": {
					""COMMAND_START"": [
						""Use"",
						""Utilize"",
						""Put"",
						""Hold""
					],
					""COMMAND_JOIN"": [
						""On"",
						""With""
					]
				},
				""items"": [
					""ITEM_TO_USE"",
					""ITEM_TO_USE_ON""
				]
			}
		},
		""messages"": {
			""NOTE"": {
				""template"": {
					""lines"": [
						{
							""special"": ""horizontal-rule""
						},
						{
							""input"": ""BODY""
						},
						{
							""special"": ""horizontal-rule""
						}
					]
				},
				""lines"": [
					""BODY""
				]
			}
		}
	},
	""player"": {
		""area"": ""HOTEL_ROOM"",
		""items"": {
			""PHONE"": {
				""names"": [
					""Phone"",
					""My phone"",
					""Cellphone"",
					""My cellphone"",
					""Cell phone"",
					""My cell phone""
				],
				""commands"": {
					""INSPECT"": {
						""actions"": {
							""messages"": ""Looks fine. No surface damage.""
						}
					},
					""USE"": {
						""actions"": {
							""messages"": ""You click the power button but nothing happens. Darn, definitely dead.""
						}
					}
				}
			},
			""WALLET"": {
				""names"": [
					""Wallet"",
					""My wallet""
				],
				""commands"": {
					""INSPECT"": {
						""actions"": {
							""messages"": ""You review the contents of your wallet, and it looks like everything is in order. Nothing is missing as far as you can tell.""
						}
					}
				}
			}
		}
	},
	""start"": {
		""messages"": [
			{
				""template"": ""NOTE"",
				""inputs"": {
					""lines"": {
						""BODY"": ""Boninger Works presents""
					}
				}
			},
			{
				""template"": ""NOTE"",
				""inputs"": {
					""lines"": {
						""BODY"": ""Crazy Ex""
					}
				}
			},
			{
				""lines"": ""You're lying fully-clothed on a bed. You're not exactly waking up so much as regaining consciousness. You've always enjoyed a good drink, or ten, but sometimes things get out of hand. So where did last night lead you today? Thankfully, you don't detect a hangover. That's a great start, but there isn't anyone next to you either, which means that last night probably didn't have a perfect ending.""
			},
			{
				""lines"": ""You sit up slowly, and get off the bed. You're pretty sure that you're in the penthouse suite of a nice hotel. Dang, it looks expensive here. You make a mental note to work on training your subconscious to be more frugal. At least your wallet and phone are still in your pockets, so you decide to go find the front desk and pay whatever you owe so that you can get out of here.""
			}
		]
	},
	""areas"": {
		""HOTEL_ROOM"": {
			""tests"": {
				""DOOR_USED"": [
					""${HOTEL_ROOM.DOOR.USED}"",
					""=="",
					""yes""
				]
			},
			""items"": {
				""DOOR"": {
					""names"": [
						""Door""
					],
					""commands"": {
						""INSPECT"": {
							""actions"": {
								""if"": {
									""condition"": ""!HOTEL_ROOM.DOOR_USED"",
									""true"": [
										{
											""messages"": ""You glance at the door. Something looks a bit strange.""
										},
										{
											""changes"": {
												""HOTEL_ROOM.DOOR.USED"": ""yes""
											}
										},
										{
											""triggers"": {
												""USE"": {
													""ITEM"": ""HOTEL_ROOM.DOOR""
												}
											}
										}
									],
									""false"": {
										""messages"": ""The door doesn't seem to have been modified besides the addition of the key card lock keeping you in.""
									}
								}
							}
						}
					}
				},
				""NOTE_WOODEN"": {
					""names"": [
						""Wood Sign"",
						""Sign"",
						""Door Sign"",
						""Wooden Note"",
						""Wood Note"",
						""Door Note"",
						""Board"",
						""Wooden Board"",
						""Wood Board""
					]
				}
			},
			""commands"": {
				""SURVEY"": {
					""actions"": {
						""if"": {
							""condition"": ""!HOTEL_ROOM.DOOR_USED"",
							""true"": {
								""messages"": ""This room looks pretty cool, but you just want to get out of here and go about your day. You should probably head for the door.""
							},
							""false"": {
								""if"": {
									""condition"": [
										""${HOTEL_ROOM.NOTE_WOODEN.VIEWED}"",
										""=="",
										""yes""
									],
									""true"": {
										""messages"": ""You glance around the room, and it all looks pretty normal. You still can't believe that the door is locked. Maybe there's something to be done if you take a closer look.""
									},
									""false"": {
										""messages"": [
											{
												""lines"": ""You're definitely in a luxury hotel room. The decor is airy. Basically, everything is light or white colored.""
											},
											{
												""lines"": ""There's wood flooring in the surprisingly large entry area, which gives way to white squishy carpet in the rest of the room. There's a big, flat-screen TV mounted on the wall with a marble-framed fireplace underneath it. Across from the TV is a sofa and coffee table. The coffee table is glass, resting on top of a block of marble that matches the fireplace, and the couch is packed with fluffy, white cushions.""
											},
											{
												""lines"": ""On the other wall is the king-sized bed that you woke up on, with a white comforter and more pillows than you can count at a glance. There's a modern nightstand on one side of the bed, and the safe on the other. Next to the bed is a painting of a peaceful, forest clearing which hangs over a small bookshelf.""
											},
											{
												""lines"": ""Finally, there's a simple but professional work desk in the corner next to a large window.""
											}
										]
									}
								}
							}
						}
					}
				}
			}
		}
	},
	""end"": {
		""messages"": [
			{
				""lines"": ""You use the titanium key to open the safe, and not a moment too soon. It's difficult to tell if your trembling is from nervous excitement that your trial may finally be over, or if it's because whatever you drank last night is starting to kill you. Either way, you grab the antidote, which looks like an EpiPen but has no distinguishing markings, and quickly read the side.""
			},
			{
				""template"": ""NOTE"",
				""inputs"": {
					""lines"": {
						""BODY"": ""Inject directly into buttocks.""
					}
				}
			},
			{
				""lines"": ""Decency will not cost you your life, so you quickly pull down your pants and jam the needle into your butt. As you wait a few seconds for the injection to finish, you mentally reconfirm your preference for a morning routine that results in a cup of coffee. Still, you are thankful to be alive.""
			},
			{
				""lines"": ""You take a few minutes to compose yourself after everything that just happened. In a weird way, you feel a sense of relief beyond just escaping this trial alive. Somehow, you're confident that your relationship is finally over. Perhaps you should be angry and report this insanity to the police, but you suspect that you would be mocked more than taken seriously. You decide what's done is done, grab the key card from the safe to unlock the door, and head down to the lobby.""
			},
			{
				""lines"": ""You arrive at the front desk, and give the receptionist your room number.""
			},
			{
				""lines"": ""Thank you. Let me see... ah yes, this is all paid for. You're good to go! Did you have a pleasant stay? When your spouse made the reservation, it seemed like there was quite a lot of fun in store for the two of you.""
			},
			{
				""lines"": ""No, you reply, actually, we ended our relationship.""
			},
			{
				""lines"": ""Oh, I'm so sorry to hear that. I apologize for asking.""
			},
			{
				""lines"": ""Don't be, you say, it was a long time coming.""
			}
		]
	}
}";
	}
}
import { IGame } from "./types/game";
import { item } from "./utilities/helpers";

export const EXAMPLE: IGame = {
	development: {
		testCommands: [
			"look around",
			"is it grass",
			"is it you",
			"is it sky"
		]
	},
	options: {
		ignoredCharacters: ["?", "!", ".", ","]
	},
	commands: [
		{
			id: "LOOK",
			parts: [
				["look around", "look", "observe"]
			]
		},
		{
			id: "CHOOSE",
			parts: [
				["is it a", "is it an", "is it the", "is it", "point at", "choose"],
				item("ITEM")
			]
		}
	],
	fail: {
		messages: "What? That doesn't even make any sense."
	},
	player: {
		id: "PLAYER",
		names: ["Self"],
		reactions: [
			{
				command: "LOOK",
				actions: {
					messages: "You look in every direction. It's pretty much just grass and clear skies all around you."
				}
			}
		]
	},
	start: [
		{
			messages: [
				{
					lines: [
						"===============",
						"|             |",
						"|    I Spy    |",
						"|             |",
						"==============="
					]
				},
				{
					lines: [
						"How to play I Spy:",
						"1) Someone says that they spy something with their litte eye.",
						"2) The other person can LOOK AROUND, or ask IS IT something that they think is what the first person spied.",
						"3) If at first you don't succeed, try, try, again."
					]
				},
				"You're in a grassy field with your best friend in the world.",
				"Suddenly, your friend turns to you and says:",
				"I spy, with my little eye, something...",
				"blue!",
				"What do you think I spied?!"
			]
		}
	],
	areas: [
		{
			id: "OUTSIDE",
			names: ["Outside"],
			items: [
				{
					id: "SKY",
					names: ["Sky", "Blue sky", "Skies", "Blue skies"],
					reactions: [
						{
							command: "CHOOSE",
							actions: [
								{
									messages: [
										"Yes! It's like you can read my mind.",
										"Now, I say we get outta here and stop messing around.",
										{
											lines: [
												"=============================",
												"|                           |",
												"|    Thanks for playing!    |",
												"|                           |",
												"============================="
											]
										}
									]
								},
								{
									special: "END"
								}
							]
						}
					]
				},
				{
					id: "GRASS",
					names: ["Grass", "Green grass"],
					reactions: [
						{
							command: "CHOOSE",
							actions: {
								messages: "No way! That's not what I spied."
							}
						}
					]
				},
				{
					id: "FRIEND",
					names: ["Your friend", "Friend", "You"],
					reactions: [
						{
							command: "CHOOSE",
							actions: {
								messages: "Me?! What kind of meta game do you think this is?"
							}
						}
					]
				}
			]
		}
	]
}

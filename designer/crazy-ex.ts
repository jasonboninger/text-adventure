import { ITextAdventure } from "./text-adventure";

export const CRAZY_EX: ITextAdventure = {
	templates: {
		actions: {
			"survey": {
				template: "${{word}}",
				words: [
					"Survey",
					"Look around",
					"Lookaround",
					"Look",
					"Observe"
				]
			}
		},
		messages: {
			"note": {
				template: {
					lines: [
						{ special: "horizontal-rule" },
						"${{input}}",
						{ special: "horizontal-rule" }
					]
				}
			}
		}
	},
	inventory: {
		"phone": {
			names: [
				"Phone",
				"My phone",
				"Cellphone",
				"My cellphone",
				"Cell phone",
				"My cell phone"
			]
		},
		"wallet": {
			names: [
				"Wallet",
				"My wallet"
			]
		}
	},
	start: {
		messages: [
			{
				template: "note",
				inputs: "Boninger Works presents"
			},
			{
				template: "note",
				inputs: "Crazy Ex"
			},
			{
				lines: "You're lying fully-clothed on a bed. You're not exactly waking up so much as regaining consciousness. You've always enjoyed a good drink, or ten, but sometimes things get out of hand. So where did last night lead you today? Thankfully, you don't detect a hangover. That's a great start, but there isn't anyone next to you either, which means that last night probably didn't have a perfect ending."
			},
			{
				lines: "You sit up slowly, and get off the bed. You're pretty sure that you're in the penthouse suite of a nice hotel. Dang, it looks expensive here. You make a mental note to work on training your subconscious to be more frugal. At least your wallet and phone are still in your pockets, so you decide to go find the front desk and pay whatever you owe so that you can get out of here."
			}
		]
	}
}

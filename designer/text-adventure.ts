import { IDictionary, IOneOrArray } from "./core";
import { IItem } from "./item";
import { IMessageTemplate } from "./message-template";
import { IActionTemplate } from "./action-template";
import { XActions } from "./action";

export interface ITextAdventure {
	templates?: {
		actions?: IDictionary<IActionTemplate>;
		messages?: IDictionary<IMessageTemplate>;
	};
	inventory?: IDictionary<IItem>;
	start?: XActions;
	end?: XActions;
}

import { IDictionary, IOneOrArray } from "./core";
import { XAction } from "./action";

export interface IReaction {
	command: string;
	items?: IDictionary<string>;
	actions: IOneOrArray<XAction>;
}

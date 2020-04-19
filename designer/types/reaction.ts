import { IDictionary, IOneOrArray } from "./core";
import { XAction } from "./action";

export interface IReaction {
	items?: IDictionary<string>;
	actions: IOneOrArray<XAction>;
}

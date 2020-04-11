import { IDictionary, IOneOrArray } from "./core";
import { XAction } from "./action";

export interface ICommand {
	items?: IDictionary<string>;
	actions: IOneOrArray<XAction>;
}

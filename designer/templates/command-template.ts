import { IDictionary, IOneOrArray } from "../core";

export interface ICommandTemplate {
	template: IOneOrArray<string>;
	words: IDictionary<IOneOrArray<string>>;
	items?: IOneOrArray<string>;
}

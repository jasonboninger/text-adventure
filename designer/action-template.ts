import { IOneOrArray, IOneOrDictionary } from "./core";

export interface IActionTemplate {
	template: string;
	words?: IOneOrDictionary<IOneOrArray<string>>;
}

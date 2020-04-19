import { IDictionary, IOneOrArray } from "./core";

export interface ICommand {
	parts: IOneOrArray<string>;
	words?: IDictionary<IOneOrArray<string>>;
	items?: IOneOrArray<string>;
}

import { IDictionary, IOneOrArray } from "./core";

export interface ICommand {
	id: string;
	parts: IOneOrArray<string>;
	words?: IDictionary<IOneOrArray<string>>;
	items?: IOneOrArray<string>;
	areas?: IOneOrArray<string>;
}

import { IDictionary, IOneOrArray } from "../core";

export interface ICommandTemplate {
	parts: IOneOrArray<string>;
	words?: IDictionary<IOneOrArray<string>>;
	items?: IOneOrArray<string>;
}

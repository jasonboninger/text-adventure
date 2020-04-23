import { IOneOrArray, IDictionary } from "./core";
import { IReaction } from "./reaction";

export interface IItem {
	names: IOneOrArray<string>;
	active?: boolean;
	reactions?: IOneOrArray<IReaction>;
	items?: IDictionary<IItem>;
}

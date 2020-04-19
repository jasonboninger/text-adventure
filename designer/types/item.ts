import { IDictionary, IOneOrArray } from "./core";
import { IReaction } from "./reaction";

export interface IItem {
	names: IOneOrArray<string>;
	active?: boolean;
	reactions?: IDictionary<IReaction>;
	items?: IOneOrArray<IItem>;
}

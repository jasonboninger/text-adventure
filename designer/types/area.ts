import { IDictionary, IOneOrArray } from "./core";
import { IItem } from "./item";
import { IReaction } from "./reaction";

export interface IArea {
	names: IOneOrArray<string>;
	reactions?: IOneOrArray<IReaction>;
	items?: IDictionary<IItem>;
}

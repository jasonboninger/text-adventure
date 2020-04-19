import { IDictionary, IOneOrArray } from "./core";
import { IItem } from "./item";
import { IReaction } from "./reaction";

export interface IArea {
	items?: IDictionary<IItem>;
	reactions?: IDictionary<IOneOrArray<IReaction>>;
}

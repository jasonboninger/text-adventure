import { IDictionary, IOneOrArray } from "./core";
import { IItem } from "./item";
import { IReaction } from "./reaction";

export interface IArea {
	id: string;
	names: string[];
	reactions?: IOneOrArray<IReaction>;
	items?: IItem[];
}

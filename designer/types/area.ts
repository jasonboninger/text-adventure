import { IDictionary, IOneOrArray } from "./core";
import { IItem } from "./item";
import { IReaction } from "./reaction";

export interface IArea {
	id: string;
	names: IOneOrArray<string>;
	reactions?: IOneOrArray<IReaction>;
	items?: IDictionary<IItem>;
}

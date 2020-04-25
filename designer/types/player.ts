import { IDictionary, IOneOrArray } from "./core";
import { IItem } from "./item";
import { IReaction } from "./reaction";

export interface IPlayer {
	area: string;
	items?: IDictionary<IItem>;
	reactions?: IOneOrArray<IReaction>;
}

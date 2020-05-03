import { XOneOrArray } from "./core";
import { IItem } from "./item";
import { IReaction } from "./reaction";

export interface IPlayer {
	id: string;
	names: string[];
	area: string;
	items?: IItem[];
	reactions?: XOneOrArray<IReaction>;
}

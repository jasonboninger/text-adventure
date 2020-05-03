import { XOneOrArray } from "./core";
import { IReaction } from "./reaction";

export interface IItem {
	id: string;
	names: string[];
	active?: boolean;
	reactions?: XOneOrArray<IReaction>;
	items?: IItem[];
}

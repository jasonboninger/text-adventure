import { IOneOrArray } from "./core";
import { IReaction } from "./reaction";

export interface IItem {
	id: string;
	names: string[];
	active?: boolean;
	reactions?: IOneOrArray<IReaction>;
	items?: IItem[];
}

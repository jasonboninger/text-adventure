import { IOneOrArray } from "./core";

export interface IItem {
	names: IOneOrArray<string>;
	discovered?: boolean;
	area?: string;
	items?: IItem;
}

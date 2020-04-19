import { IDictionary } from "./core";
import { IItem } from "./item";

export interface IPlayer {
	area: string;
	items?: IDictionary<IItem>;
}

import { IDictionary, IOneOrArray } from "./core";
import { IItem } from "./item";
import { IConditions } from "./condition";
import { ICommand } from "./command";

export interface IArea {
	items?: IDictionary<IItem>;
	tests?: IDictionary<IConditions>;
	commands?: IDictionary<IOneOrArray<ICommand>>;
}

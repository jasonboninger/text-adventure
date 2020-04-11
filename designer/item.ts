import { IDictionary, IOneOrArray } from "./core";
import { IConditions } from "./condition";
import { ICommand } from "./command";

export interface IItem {
	names: IOneOrArray<string>;
	active?: boolean;
	tests?: IDictionary<IConditions>;
	commands?: IDictionary<ICommand>;
	items?: IOneOrArray<IItem>;
}

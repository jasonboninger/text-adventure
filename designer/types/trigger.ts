import { XOneOrArray } from "./core";
import { IInput } from "./input";

export interface ITrigger {
	command: string;
	inputs?: XOneOrArray<IInput>;
}

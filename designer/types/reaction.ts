import { XOneOrArray } from "./core";
import { XAction } from "./action";
import { IInput } from "./input";

export interface IReaction {
	command: string;
	inputs?: XOneOrArray<IInput>;
	actions: XOneOrArray<XAction>;
}

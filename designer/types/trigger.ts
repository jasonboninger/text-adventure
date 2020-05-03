import { XDictionary } from "./core";

export interface ITrigger {
	command: string;
	items?: XDictionary<string>;
}

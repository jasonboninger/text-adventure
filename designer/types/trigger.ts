import { IDictionary } from "./core";

export interface ITrigger {
	command: string;
	items?: IDictionary<string>;
}

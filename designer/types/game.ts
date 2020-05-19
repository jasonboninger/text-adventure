import { XOneOrArray } from "./core";
import { ICommand } from "./command";
import { IPlayer } from "./player";
import { IArea } from "./area";
import { XAction } from "./action";
import { IConditionArea, IConditionItem } from "./condition";
import { IOptions } from "./options";
import { IDevelopment } from "./development";

export interface IGame {
	commands: ICommand[];
	player: IPlayer;
	areas?: IArea[];
	start?: XOneOrArray<XAction>;
	end?: XOneOrArray<XAction>;
	fallback?: XOneOrArray<XAction>;
	prompt?: XOneOrArray<XAction>;
	areaInContext?: IConditionArea;
	itemInContext?: IConditionItem;
	areaAmbiguous?: XOneOrArray<XAction>;
	itemAmbiguous?: XOneOrArray<XAction>;
	options?: IOptions;
	development?: IDevelopment;
}

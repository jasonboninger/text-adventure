import { IDictionary, IOneOrArray } from "./core";
import { ICommandTemplate } from "./templates/command-template";
import { IMessageTemplate } from "./templates/message-template";
import { IPlayer } from "./player";
import { IArea } from "./area";
import { IConditions } from "./condition";
import { XAction } from "./action";

export interface IGame {
	templates: {
		commands: IDictionary<ICommandTemplate>;
		messages?: IDictionary<IMessageTemplate>;
	};
	player: IPlayer;
	tests?: IDictionary<IConditions>;
	start?: IOneOrArray<XAction>;
	areas: IDictionary<IArea>;
	end?: IOneOrArray<XAction>;
}

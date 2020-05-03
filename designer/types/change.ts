export type XChange = IChangeStandard | IChangeCustom;

export interface IChangeStandard {
	target: string;
	standard: string;
	custom?: never;
	value: string;
}

export interface IChangeCustom {
	target: string;
	standard?: never;
	custom: string;
	value: string;
}

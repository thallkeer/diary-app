import { IHabitTracker } from "./Lists/habitTracker";
import { ICommonList, ITodoList } from "./Lists/lists";

export interface IEntity {
	id: number;
}

export interface IUser extends IEntity {
	username: string;
	password?: string;
	token?: string;
	telegramId?: string;
}

export interface PageAreaTransferSettings {
	id?: number;
	transferGoalsArea?: boolean;
	transferPurchasesArea?: boolean;
	transferDesiresArea?: boolean;
	transferIdeasArea?: boolean;
}

export interface NotificationsSettings {
	id: number;
	isActivated: boolean;
	notifyDayBefore: boolean;
	notifyAt: string;
}

export interface IUserSettings {
	id: number;
	userId: number;
	pageAreaTransferSettings: PageAreaTransferSettings;
	notificationSettings: NotificationsSettings;
}

export interface IMonthAreaList<TList> extends IEntity {
	areaOwnerId: number;
	list: TList;
}

export interface IPurchaseList extends IMonthAreaList<ITodoList> {}

export interface IDesireList extends IMonthAreaList<ICommonList> {}

export interface IIdeasList extends IMonthAreaList<ICommonList> {}

export interface IGoalList extends IMonthAreaList<IHabitTracker> {}

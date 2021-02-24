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
}

export interface IUserSettings {
	id: number;
	userId: number;
	pageAreaTransferSettings: PageAreaTransferSettings;
	notificationSettings: NotificationsSettings;
}

export interface IDiaryListWrapper extends IEntity {
	areaOwnerId: number;
}

export interface IPurchaseList extends IDiaryListWrapper {
	list: ITodoList;
}

export interface IDesireList extends IDiaryListWrapper {
	list: ICommonList;
}

export interface IIdeasList extends IDiaryListWrapper {
	list: ICommonList;
}

export interface IGoalList extends IDiaryListWrapper {
	list: IHabitTracker;
}

import { IHabitTracker } from "./Lists/habitTracker";
import { ICommonList, ITodoList } from "./Lists/lists";

export interface IEntity {
	id: number;
}

export interface IUser extends IEntity {
	username: string;
	password?: string;
	token?: string;
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

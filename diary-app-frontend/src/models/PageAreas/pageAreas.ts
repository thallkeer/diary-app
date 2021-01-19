import {
	IDesireList,
	IEntity,
	IIdeasList,
	IPurchaseList,
} from "models/entities";
import { IHabitTracker } from "models/Lists/habitTracker";
import { IEventList, ITodoList } from "models/Lists/lists";

export interface IPageArea extends IEntity {
	header: string;
}

export interface IImportantEventsArea extends IPageArea {
	importantEvents: IEventList;
}

export interface IImportantThingsArea extends IPageArea {
	importantThings: ITodoList;
}
export interface IPurchasesArea extends IPageArea {
	purchasesLists: IPurchaseList[];
}

export interface IDesiresArea extends IPageArea {
	desiresLists: IDesireList[];
}

export interface IIdeasArea extends IPageArea {
	ideasList: IIdeasList;
}

export interface IGoalsArea extends IPageArea {
	goalLists: IHabitTracker[];
}

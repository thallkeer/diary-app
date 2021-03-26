import { IEntity } from "models/entities";
import {
	ICommonList,
	IEventList,
	IHabitTracker,
	ITodoList,
} from "models/Lists/lists";

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
	purchasesLists: ITodoList[];
}

export interface IDesiresArea extends IPageArea {
	desiresLists: ICommonList[];
}

export interface IIdeasArea extends IPageArea {
	ideasList: ICommonList;
}

export interface IGoalsArea extends IPageArea {
	goalLists: IHabitTracker[];
}

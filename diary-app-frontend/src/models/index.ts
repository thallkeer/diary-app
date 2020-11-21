import {
	IStateWithLoading,
} from "../context/reducers/utilities/loading-reducer";

export interface IList<T extends IListItem> {
	id: number;
	pageID: number;
	title: string;
	items: T[];
}

export interface IListState<T extends IList<TItem>, TItem extends IListItem>
	extends IStateWithLoading {
	list: T;
	listName: string;
}

export type List = ITodoList | IEventList | ICommonList;
export interface ITodoList extends IList<ITodo> {}
export interface IEventList extends IList<IEvent> {}

export interface ICommonList extends IList<IListItem> {}

export interface IListItem {
	id: number;
	subject: string;
	url: string;
	ownerID?: number;
	readonly?: boolean;
}

export interface ITodo extends IListItem {
	done: boolean;
}

export interface IEvent extends IListItem {
	date: Date;
	fullDay?: boolean;
	description?: string;
}

export type HabitDay = {
	number: number;
	note: string;
};

export interface IHabitsTracker {
	id: number;
	goalName: string;
	goalsAreaId: number;
	selectedDays: HabitDay[];
}

export interface IUser {
	id: number;
	username: string;
	password?: string;
	token?: string;
}

export interface IPage {
	id: number;
}

export interface IPageState<T extends IPage> extends IStateWithLoading {
	page: T;
	pageName: string;
}

export interface IPageArea {
	id: number;
	header: string;
}

export interface IPageAreaState extends IStateWithLoading {
	area: IPageArea
	pageAreaName: string;
}


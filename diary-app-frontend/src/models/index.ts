import { ITodoListState } from "../components/Lists/TodoList/TodoListState";
import { IEventListContext } from "../components/Lists/EventList/EventListState";

export interface IList<T extends IListItem> {
	id: number;
	pageID: number;
	title: string;
	items: T[];
}

export interface IListState<T extends IList<TItem>, TItem extends IListItem> {
	list: T;
	loading: boolean;
}

export type List = ITodoList | IEventList | ICommonList;
export interface ITodoList extends IList<ITodo> {
	purchasesAreaId?: number;
}
export interface IEventList extends IList<IEvent> {
	ideasAreaID?: number;
	desiresAreaID?: number;
}

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

export type ListItem = ITodo | IEvent | IListItem;

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
	year: number;
	month: number;
	user: IUser;
}

export interface IMainPage {
	page: IPage;
	todos: ITodoListState;
	events: IEventListContext;
}

export interface IMonthPage {
	page: IPage;
	purchasesArea: IPurchasesArea;
	desiresArea: IDesiresArea;
	ideasArea: IIdeasArea;
	goalsArea: IGoalsArea;
}

export interface IPageArea {
	id: number;
	header: string;
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
	goalsLists: IHabitsTracker[];
}

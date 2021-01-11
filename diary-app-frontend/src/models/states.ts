import { ICommonListState } from "../context/reducers/list/commonLists";
import { IHabitTrackerState } from "../context/reducers/list/habitTrackers";
import { ITodoListState } from "../context/reducers/list/todos";
import {
	IDiaryList,
	IListItem,
	IPage,
	IPageArea,
	IImportantEventsArea,
	IImportantThingsArea,
	IPurchasesArea,
	IDesiresArea,
	IIdeasArea,
	IHabitTracker,
	IGoalsArea,
	IListWithItems,
} from "./entities";
import { PageAreaNames, PageNames } from "./types";

export interface IStateWithLoading {
	error: false;
	isLoading: false;
	success: false;
}

export interface IPageState<T extends IPage> extends IStateWithLoading {
	page: T;
	pageName: PageNames;
}

export interface IPageAreaState<T extends IPageArea> extends IStateWithLoading {
	area: T;
	pageAreaName: PageAreaNames;
}

export interface IImportantEventsAreaState
	extends IPageAreaState<IImportantEventsArea> {}

export interface IImportantThingsAreaState
	extends IPageAreaState<IImportantThingsArea> {}

export interface IListState<TList extends IListWithItems<TItem>, TItem> {
	list: TList;
	listName: string;
}

export interface IDiaryListState<
	TList extends IDiaryList<TItem>,
	TItem extends IListItem
> extends IListState<TList, TItem> {}

export interface IPurchaseListState {
	purchaseListId: number;
	purchaseAreaId: number;
	listState: ITodoListState;
}

export interface IDesireListState {
	desireListId: number;
	desireAreaId: number;
	listState: ICommonListState;
}

export interface IIdeasListState {
	ideasListId: number;
	ideasAreaId: number;
	listState: ICommonListState;
}

export interface ListsStateByName<T> {
	[listName: string]: T;
}

export interface IDiaryListWrapperCollectionState<T> {
	byName: ListsStateByName<T>;
}

export interface IPurchasesAreaState extends IPageAreaState<IPurchasesArea> {}
export interface IPurchaseListsState
	extends IDiaryListWrapperCollectionState<IPurchaseListState> {}

export interface IDesiresAreaState extends IPageAreaState<IDesiresArea> {}
export interface IDesireListsState
	extends IDiaryListWrapperCollectionState<IDesireListState> {}

export interface IIdeasAreaState extends IPageAreaState<IIdeasArea> {}

export interface IGoalsAreaState extends IPageAreaState<IGoalsArea> {}
export interface IGoalsListsState
	extends IDiaryListWrapperCollectionState<IHabitTrackerState> {}

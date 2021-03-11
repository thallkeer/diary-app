import { IDiaryList, IListItem, IListWithItems } from "./Lists/lists";

export interface IStateWithLoading {
	error: false;
	isLoading: false;
	success: false;
}

export interface IListState<TList extends IListWithItems<TItem>, TItem> {
	list: TList;
	listName: string;
}

export interface IDiaryListState<
	TList extends IDiaryList<TItem>,
	TItem extends IListItem
> extends IListState<TList, TItem> {}

export interface ListsStateByName<T> {
	[listName: string]: T;
}

import { IListWithItems } from "models";
import { IDiaryListWrapper, IEntity } from "../../models/entities";
import {
	IDiaryListWrapperCollectionState,
	IListState,
	ListsStateByName,
} from "../../models/states";
import { getListsCollectionSelector } from "../pages/pages.selectors";

export abstract class ListCollectionHandler<
	TListCollectionState extends IDiaryListWrapperCollectionState<TListWrapperState>,
	TListWrapperState,
	TListWrapper extends IDiaryListWrapper,
	TListState extends IListState<TList, TItem>,
	TList extends IListWithItems<TItem>,
	TItem extends IEntity,
	TReducer
> {
	private reducers: Map<string, TReducer>;
	protected abstract listNamePrefix: string;

	constructor() {
		this.reducers = new Map<string, TReducer>();
	}

	protected abstract createListReducer(listName: string): TReducer;

	protected abstract listStateCreator(list: TListWrapper): TListWrapperState;

	/**
	 * Generates name for list to identify it in reducer
	 * @param listId List unique identifier
	 */
	public getListName = (listId: number) => this.listNamePrefix + "_" + listId;

	/**
	 * Creates new reducer and state for given list wrapper
	 * @param list List wrapper entity
	 */
	public createListState = (list: TListWrapper): TListWrapperState => {
		const listName = this.getListName(list.id);
		const reducer = this.createListReducer(listName);
		this.reducers.set(listName, reducer);
		const listState = this.listStateCreator(list);
		return listState;
	};

	/**
	 * Handles set action. Initializes state with given lists.
	 * @param lists Lists collection
	 */
	public handleSetLists = (lists: TListWrapper[]): TListCollectionState => {
		this.reducers.clear();
		const newState: ListsStateByName<TListWrapperState> = {};

		lists.forEach((list) => {
			const listName = this.getListName(list.id);
			newState[listName] = this.createListState(list);
		});
		return {
			byName: newState,
		} as TListCollectionState;
	};

	/**
	 * Handles add action. Creates new list state and adds it to current state.
	 * @param currentState
	 * @param list
	 */
	public handleAddList = (
		currentState: TListCollectionState,
		list: TListWrapper
	): TListCollectionState => {
		const addedState = this.createListState(list);
		const plName = this.getListName(list.id);
		return {
			...currentState,
			byName: {
				...currentState.byName,
				[plName]: addedState,
			},
		};
	};

	/**
	 * Deletes list from state.
	 * @param currentState
	 * @param listId
	 */
	handleDeleteList = (
		currentState: TListCollectionState,
		listId: number
	): TListCollectionState => {
		const listName = this.getListName(listId);
		const stateAfterDelete = { ...currentState };
		delete stateAfterDelete.byName[listName];
		return stateAfterDelete;
	};

	/**
	 * Handles list crud actions.
	 * @param currentState
	 * @param listKey
	 * @param reduce
	 */
	handleListAction = (
		currentState: TListCollectionState,
		listKey: string,
		reduce: (reducer: TReducer, list: TListWrapperState) => TListState
	): TListCollectionState => {
		const reducer = this.reducers.get(listKey);
		if (!reducer) return currentState;
		const listsByName = getListsCollectionSelector<
			TListCollectionState,
			TListWrapperState
		>(currentState);
		const list = listsByName[listKey];

		const newListState = reduce(reducer, list);
		return {
			...currentState,
			byName: {
				...currentState.byName,
				[listKey]: {
					...list,
					listState: newListState,
				},
			},
		};
	};
}

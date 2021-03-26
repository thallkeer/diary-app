import { IList } from "models";
import { IEntity } from "../../models/entities";
import { IListState, ListsStateByName } from "../../models/states";

export class ReducerCollection<
	TReducer,
	TListState extends IListState<TList, TItem>,
	TList extends IList<TItem>,
	TItem extends IEntity
> {
	private reducers: Map<string, TReducer>;
	private initialState: TListState;
	private createReducer: (reducerName: string) => TReducer;
	private reducerNamePrefix: string;

	constructor(
		initialState: TListState,
		createReducer: (reducerName: string) => TReducer,
		reducerNamePrefix: string
	) {
		this.initialState = initialState;
		this.createReducer = createReducer;
		this.reducerNamePrefix = reducerNamePrefix;
		this.reducers = new Map<string, TReducer>();
	}

	/**
	 * Creates new reducer and state for given entity
	 * @param entityKey
	 * @param entity
	 * @returns
	 */
	public add = (entityKey: number, entity: TList): TListState => {
		const reducerName = this.getReducerName(entityKey);
		const reducer = this.createReducer(reducerName);

		this.reducers.set(reducerName, reducer);

		return {
			...this.initialState,
			list: entity,
		};
	};

	/**
	 * Returns reducer by given name
	 * @param reducerName name of reducer
	 * @returns
	 */
	public get(reducerName: string) {
		return this.reducers.get(reducerName);
	}

	/**
	 * Removes all reducers from collection
	 */
	public clear() {
		this.reducers.clear();
	}

	/**
	 * Generates name for entity to identify it in reducer
	 * @param entityKey Entity unique identifier
	 */
	public getReducerName = (entityKey: number) =>
		this.reducerNamePrefix + "_" + entityKey;
}

export class ListCollectionHandler<
	TListState extends IListState<TList, TItem>,
	TList extends IList<TItem>,
	TItem extends IEntity,
	TReducer
> {
	private reducers: ReducerCollection<TReducer, TListState, TList, TItem>;

	constructor(reducers: ReducerCollection<TReducer, TListState, TList, TItem>) {
		this.reducers = reducers;
	}

	/**
	 * Returns name of reducer for list with given id
	 * @param listId id of list
	 * @returns
	 */
	public getListName(listId: number): string {
		return this.reducers.getReducerName(listId);
	}

	/**
	 * Handles set action. Initializes state with given lists.
	 * @param lists Lists collection
	 */
	public handleSetLists = (lists: TList[]): ListsStateByName<TListState> => {
		this.reducers.clear();
		const newState: ListsStateByName<TListState> = {};

		lists.forEach((list) => {
			const listName = this.reducers.getReducerName(list.id);
			newState[listName] = this.reducers.add(list.id, list);
		});

		return newState;
	};

	/**
	 * Handles add action. Creates new list state and adds it to current state.
	 * @param currentState
	 * @param list
	 */
	public handleAddList = (
		currentState: ListsStateByName<TListState>,
		list: TList
	): ListsStateByName<TListState> => {
		const addedState = this.reducers.add(list.id, list);
		const plName = this.reducers.getReducerName(list.id);

		return {
			...currentState,
			[plName]: addedState,
		};
	};

	/**
	 * Deletes list reducer from state.
	 * @param currentState
	 * @param listId
	 */
	handleDeleteList = (
		currentState: ListsStateByName<TListState>,
		listId: number
	): ListsStateByName<TListState> => {
		const listName = this.reducers.getReducerName(listId);
		const stateAfterDelete = { ...currentState };
		delete stateAfterDelete[listName];
		return stateAfterDelete;
	};

	/**
	 * Handles concrete list crud actions.
	 * @param currentState
	 * @param listKey
	 * @param reduce
	 */
	handleListAction = (
		currentState: ListsStateByName<TListState>,
		listKey: string,
		reduce: (reducer: TReducer, listState: TListState) => TListState
	): ListsStateByName<TListState> => {
		const reducer = this.reducers.get(listKey);

		if (!reducer) return currentState;

		const listState = currentState[listKey];
		const newListState = reduce(reducer, listState);
		return {
			...currentState,
			[listKey]: newListState,
		};
	};
}

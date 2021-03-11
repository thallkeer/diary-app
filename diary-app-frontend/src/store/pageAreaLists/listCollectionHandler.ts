import { IEntity, IMonthAreaList } from "../../models/entities";
import { ListsStateByName } from "../../models/states";

export abstract class ReducerCollection<TReducer, TEntity, TState> {
	private reducers: Map<string, TReducer>;
	protected abstract reducerNamePrefix: string;

	constructor() {
		this.reducers = new Map<string, TReducer>();
	}

	protected abstract createReducer(reducerName: string): TReducer;

	protected abstract createState(entity: TEntity): TState;

	/**
	 * Creates new reducer and state for given entity
	 * @param list List wrapper entity
	 */
	public add = (entityKey: number, entity: TEntity): TState => {
		const listName = this.getReducerName(entityKey);
		const reducer = this.createReducer(listName);
		this.reducers.set(listName, reducer);
		return this.createState(entity);
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
	 * Removes all reducer from collection
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
	TListWrapperState,
	TListWrapper extends IEntity,
	TListState,
	TReducer
> {
	private reducers: ReducerCollection<
		TReducer,
		TListWrapper,
		TListWrapperState
	>;

	constructor(
		reducers: ReducerCollection<TReducer, TListWrapper, TListWrapperState>
	) {
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
	public handleSetLists = (
		lists: TListWrapper[]
	): ListsStateByName<TListWrapperState> => {
		this.reducers.clear();
		const newState: ListsStateByName<TListWrapperState> = {};

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
		currentState: ListsStateByName<TListWrapperState>,
		list: TListWrapper
	): ListsStateByName<TListWrapperState> => {
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
		currentState: ListsStateByName<TListWrapperState>,
		listId: number
	): ListsStateByName<TListWrapperState> => {
		const listName = this.reducers.getReducerName(listId);
		const stateAfterDelete = { ...currentState };
		delete stateAfterDelete.byName[listName];
		return stateAfterDelete;
	};

	/**
	 * Handles concrete list crud actions.
	 * @param currentState
	 * @param listKey
	 * @param reduce
	 */
	handleListAction = (
		currentState: ListsStateByName<TListWrapperState>,
		listKey: string,
		reduce: (reducer: TReducer, list: TListWrapperState) => TListState
	): ListsStateByName<TListWrapperState> => {
		const reducer = this.reducers.get(listKey);
		if (!reducer) return currentState;

		const list = currentState[listKey];
		const newListState = reduce(reducer, list);
		return {
			...currentState,
			[listKey]: {
				...list,
				listState: newListState,
			},
		};
	};
}

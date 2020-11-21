import { ActionsUnion, createNamedAction } from "./action-helpers";
import { IListItem, IList, IListState } from "../../models/index";
import { getListService } from "../../services/lists";
import { createPropSelector, getListItems } from "../../selectors";
import { BaseThunkType, createNamedWrapperReducer } from "../store";
import withLoadingStates from "../reducers/utilities/loading-reducer";

const LOAD_LIST_START = "LOAD_LIST_START";
const LOAD_LIST_SUCCESS = "LOAD_LIST_SUCCESS";
const LOAD_LIST_ERROR = "LOAD_LIST_ERROR";
const ADD_LIST_ITEM = "ADD_LIST_ITEM";
const UPDATE_LIST_ITEM = "UPDATE_LIST_ITEM";
const DELETE_LIST_ITEM = "DELETE_LIST_ITEM";
const DELETE_LIST = "DELETE_LIST";
const UPDATE_LIST = "UPDATE_LIST";

export function createNamedWrapperListReducer<
	TState extends IListState<TList, TListItem>,
	TList extends IList<TListItem>,
	TListItem extends IListItem
>(initialState: TState, reducerName: string) {
	return createNamedWrapperReducer(
		wrappedReducer,
		initialState,
		reducerName,
		(action: ListActions) => action.subjectName
	);
}

const wrappedReducer = withLoadingStates({
	START: LOAD_LIST_START,
	SUCCESS: LOAD_LIST_SUCCESS,
	ERROR: LOAD_LIST_ERROR,
})(listReducer);

export const withListStates = () => {
	return <
		S extends IListState<TList, TItem>,
		TList extends IList<TItem>,
		TItem extends IListItem,
		A extends ListActions
	>(
		baseReducer: (state: S | undefined, action: A) => S
	) =>
		// Returns a new reducer
		(state: S | undefined, action: A): S => {
			const nextState = wrappedReducer(state, action);
			if (nextState !== state) return nextState;
			console.log(
				"not a common action, calling additional reducer",
				state,
				action
			);

			return baseReducer(nextState, action);
		};
};

export function listReducer<
	TState extends IListState<TList, TListItem>,
	TList extends IList<TListItem>,
	TListItem extends IListItem
>(state: TState, action: ListActions): TState {
	switch (action.type) {
		case "LOAD_LIST_SUCCESS": {
			return {
				...state,
				list: action.payload,
			};
		}
		case "ADD_LIST_ITEM":
			return {
				...state,
				list: {
					...state.list,
					items: [...getListItems(state), action.payload],
				},
			};

		case "UPDATE_LIST_ITEM":
			return {
				...state,
				list: {
					...state.list,
					items: getListItems(state).map((item) =>
						item.id === action.payload.id ? action.payload : item
					),
				},
			};

		case "DELETE_LIST_ITEM":
			return {
				...state,
				list: {
					...state.list,
					items: getListItems(state).filter(
						(event) => event.id !== action.payload
					),
				},
			};

		case "UPDATE_LIST":
			return {
				...state,
				list: action.payload as TList,
			};

		case "DELETE_LIST":
			return {
				...state,
				list: null,
			};

		default:
			console.log("unknown action type reducer", action, state);
			return state;
	}
}

export const updateListItems = <
	TState extends IListState<TList, TListItem>,
	TList extends IList<TListItem>,
	TListItem extends IListItem
>(
	state: TState,
	action: ListActions,
	updateFunction: (action: ListActions, items: TListItem[]) => TState
) => {
	return {
		...state,
		list: {
			...state.list,
			items: updateFunction(action, getListItems(state)),
		},
	};
};

const actions = {
	startLoadList: (listName: string) =>
		createNamedAction(LOAD_LIST_START, listName, undefined),
	finishLoadList: <TList extends IList<TListItem>, TListItem extends IListItem>(
		list: TList,
		listName: string
	) => createNamedAction(LOAD_LIST_SUCCESS, listName, list),
	updateListItem: <TListItem extends IListItem>(
		listItem: TListItem,
		listName: string
	) => createNamedAction(UPDATE_LIST_ITEM, listName, listItem),
	addListItem: <TListItem extends IListItem>(
		newListItem: TListItem,
		listName: string
	) => createNamedAction(ADD_LIST_ITEM, listName, newListItem),
	deleteListItem: (listItemID: number, listName: string) =>
		createNamedAction(DELETE_LIST_ITEM, listName, listItemID),
	deleteList: (listID: number, listName: string) =>
		createNamedAction(DELETE_LIST, listName, listID),
	updateList: <TList extends IList<TListItem>, TListItem extends IListItem>(
		list: TList,
		listName: string
	) => createNamedAction(UPDATE_LIST, listName, list),
};

export const getListActions = <
	TList extends IList<TListItem>,
	TListItem extends IListItem
>(
	listUrl: string,
	listItemName: string
) => {
	console.log("get list actions is called", listUrl, listItemName);

	const {
		addItem,
		deleteItem,
		deleteList,
		updateItem,
		update,
	} = getListService<TList, TListItem>(listUrl, listItemName);

	const setList = (list: TList): ThunkType => async (dispatch) => {
		dispatch(actions.finishLoadList(list, listItemName));
	};

	const updateList = (list: TList): ThunkType => async (dispatch) => {
		await update(list);
		dispatch(actions.updateList(list, listItemName));
	};

	const addOrUpdateListItem = (listItem: TListItem): ThunkType => async (
		dispatch
	) => {
		if (!listItem) return;

		if (listItem.id === 0) {
			await addItem(listItem).then((listItemID) =>
				dispatch(
					actions.addListItem({ ...listItem, id: listItemID }, listItemName)
				)
			);
		} else {
			await updateItem(listItem).then((_) =>
				dispatch(actions.updateListItem(listItem, listItemName))
			);
		}
	};

	const deleteListItem = (listItemID: number): ThunkType => async (
		dispatch
	) => {
		if (listItemID === 0) return;
		await deleteItem(listItemID);
		dispatch(actions.deleteListItem(listItemID, listItemName));
	};

	const removeList = (listID: number): ThunkType => async (dispatch) => {
		await deleteList(listID);
		dispatch(actions.deleteList(listID, listItemName));
	};

	return {
		setList,
		updateList,
		addOrUpdateListItem,
		deleteListItem,
		removeList,
	};
};

export type ListActions = ActionsUnion<typeof actions>;
type ThunkType = BaseThunkType<ListActions>;

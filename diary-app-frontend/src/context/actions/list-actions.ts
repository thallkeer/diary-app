import { ActionsUnion, createNamedAction } from "./action-helpers";
import { IListItem, IList, IListState } from "../../models/index";
import { getListService } from "../../services/listService";
import { updateListInState } from "../../utils/index";
import { BaseThunkType } from "../store";
import withLoadingStates from "../reducers/utilities/loading-reducer";

const LOAD_LIST_START = "LOAD_LIST_START";
const LOAD_LIST_SUCCESS = "LOAD_LIST_SUCCESS";
const LOAD_LIST_ERROR = "LOAD_LIST_ERROR";
const ADD_LIST_ITEM = "ADD_LIST_ITEM";
const UPDATE_LIST_ITEM = "UPDATE_LIST_ITEM";
const DELETE_LIST_ITEM = "DELETE_LIST_ITEM";
const DELETE_LIST = "DELETE_LIST";
const UPDATE_LIST = "UPDATE_LIST";

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
		baseReducer: (state: S, action: A) => S
	) => (state: S, action: A): S => {
		const nextState = wrappedReducer(state, action);
		return nextState !== state ? nextState : baseReducer(nextState, action);
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
			return updateListInState(state, (listItems) => [
				...listItems,
				action.payload as TListItem,
			]);

		case "UPDATE_LIST_ITEM":
			return updateListInState(state, (listItems) =>
				listItems.map(
					(item) =>
						(item.id === action.payload.id ? action.payload : item) as TListItem
				)
			);

		case "DELETE_LIST_ITEM":
			return updateListInState(state, (listItems) =>
				listItems.filter((event) => event.id !== action.payload)
			);

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
			return state;
	}
}

export const createListActions = <
	TList extends IList<TListItem>,
	TListItem extends IListItem
>() => {
	const actions = {
		startLoadList: (listName: string) =>
			createNamedAction(LOAD_LIST_START, listName, undefined),
		finishLoadList: (list: TList, listName: string) =>
			createNamedAction(LOAD_LIST_SUCCESS, listName, list),
		updateListItem: (listItem: TListItem, listName: string) =>
			createNamedAction(UPDATE_LIST_ITEM, listName, listItem),
		addListItem: (newListItem: TListItem, listName: string) =>
			createNamedAction(ADD_LIST_ITEM, listName, newListItem),
		deleteListItem: (listItemID: number, listName: string) =>
			createNamedAction(DELETE_LIST_ITEM, listName, listItemID),
		deleteList: (listID: number, listName: string) =>
			createNamedAction(DELETE_LIST, listName, listID),
		updateList: (list: TList, listName: string) =>
			createNamedAction(UPDATE_LIST, listName, list),
	};
	return actions;
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

	const setList = (list: TList, listName: string): ThunkType => async (
		dispatch
	) => {
		dispatch(actions.finishLoadList(list, listName));
	};

	const updateList = (list: TList, listName: string): ThunkType => async (
		dispatch
	) => {
		await update(list);
		dispatch(actions.updateList(list, listName));
	};

	const addOrUpdateListItem = (
		listItem: TListItem,
		listName: string
	): ThunkType => async (dispatch) => {
		if (!listItem) return;

		if (listItem.id === 0) {
			await addItem(listItem).then((listItemID) =>
				dispatch(actions.addListItem({ ...listItem, id: listItemID }, listName))
			);
		} else {
			await updateItem(listItem).then((_) =>
				dispatch(actions.updateListItem(listItem, listName))
			);
		}
	};

	const deleteListItem = (
		listItemID: number,
		listName: string
	): ThunkType => async (dispatch) => {
		if (listItemID === 0) return;
		await deleteItem(listItemID);
		dispatch(actions.deleteListItem(listItemID, listName));
	};

	const removeList = (listID: number, listName: string): ThunkType => async (
		dispatch
	) => {
		await deleteList(listID);
		dispatch(actions.deleteList(listID, listName));
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

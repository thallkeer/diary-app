import { ActionsUnion, createAction } from "./action-helpers";
import {
	IListItem,
	IList,
	IListState,
	List,
	ListItem,
} from "../../models/index";
import { getListService } from "../../services/lists";
import { getListItems } from "../../selectors";

const ADD_LIST_ITEM = "ADD_LIST_ITEM";
const UPDATE_LIST_ITEM = "UPDATE_LIST_ITEM";
const LOAD_LIST_START = "LOAD_LIST_START";
const SET_LIST = "SET_LIST";
const DELETE_LIST_ITEM = "DELETE_LIST_ITEM";
const DELETE_LIST = "DELETE_LIST";
const UPDATE_LIST = "UPDATE_LIST";

export function getActions<
	List extends IList<ListItem>,
	ListItem extends IListItem
>() {
	return {
		startLoadList: () => createAction(LOAD_LIST_START),
		finishLoadList: (list: List) => createAction(SET_LIST, list),
		updateListItem: (listItem: ListItem) =>
			createAction(UPDATE_LIST_ITEM, listItem),
		addListItem: (newListItem: ListItem) =>
			createAction(ADD_LIST_ITEM, newListItem),
		deleteListItem: (listItemID: number) =>
			createAction(DELETE_LIST_ITEM, listItemID),
		deleteList: (listID: number) => createAction(DELETE_LIST, listID),
		updateList: (list: List) => createAction(UPDATE_LIST, list),
	};
}

const Actions = {
	startLoadList: () => createAction(LOAD_LIST_START),
	finishLoadList: (list: List) => createAction(SET_LIST, list),
	updateListItem: (listItem: ListItem) =>
		createAction(UPDATE_LIST_ITEM, listItem),
	addListItem: (newListItem: ListItem) =>
		createAction(ADD_LIST_ITEM, newListItem),
	deleteListItem: (listItemID: number) =>
		createAction(DELETE_LIST_ITEM, listItemID),
	deleteList: (listID: number) => createAction(DELETE_LIST, listID),
	updateList: (list: List) => createAction(UPDATE_LIST, list),
};

type ListActions = ActionsUnion<typeof Actions>;

export function listReducer<
	TState extends IListState<TList, TListItem>,
	TList extends IList<TListItem>,
	TListItem extends IListItem
>(state: TState, action: ListActions): TState {
	switch (action.type) {
		case "LOAD_LIST_START":
			return { ...state, loading: true };

		case "SET_LIST": {
			return {
				...state,
				list: action.payload,
				loading: false,
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
				list: action.payload,
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

export function useListActions(listUrl: string, listItemName: string) {
	const {
		addItem,
		deleteItem,
		remove,
		getByPageID,
		updateItem,
		update,
	} = getListService(listUrl, listItemName);

	function setList(list: List, dispatch: React.Dispatch<ListActions>) {
		dispatch(Actions.finishLoadList(list));
	}

	async function loadListByPageID(
		pageID: number,
		dispatch: React.Dispatch<ListActions>
	) {
		dispatch(Actions.startLoadList());
		await getByPageID(pageID).then((list: List) =>
			dispatch(Actions.finishLoadList(list))
		);
	}

	const updateList = async (
		list: List,
		dispatch: React.Dispatch<ListActions>
	) => {
		await update(list);
		dispatch(Actions.updateList(list));
	};

	const addOrUpdateListItem = async (
		listItem: ListItem,
		dispatch: React.Dispatch<ListActions>
	) => {
		if (!listItem) return;

		if (listItem.id === 0) {
			await addItem(listItem).then((listItemID) =>
				dispatch(Actions.addListItem({ ...listItem, id: listItemID }))
			);
		} else {
			await updateItem(listItem).then((_) =>
				dispatch(Actions.updateListItem(listItem))
			);
		}
	};

	async function deleteListItem(
		listItemID: number,
		dispatch: React.Dispatch<ListActions>
	) {
		if (listItemID === 0) return;
		await deleteItem(listItemID);
		dispatch(Actions.deleteListItem(listItemID));
	}

	async function removeList(
		listID: number,
		dispatch: React.Dispatch<ListActions>
	) {
		await remove(listID);
		dispatch(Actions.deleteList(listID));
	}

	return {
		setList,
		loadListByPageID,
		updateList,
		addOrUpdateListItem,
		deleteListItem,
		removeList,
	};
}

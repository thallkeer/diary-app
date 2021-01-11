import { IEntity, IListWithItems } from "../../../models/entities";
import { IListState } from "../../../models/states";
import { updateListInState } from "../../../utils";
import { createNamedAction } from "../../actions/action-helpers";
import { ListActions } from "../../actions/listCrud-actions";

const SET_LIST = "SET_LIST";
const DELETE_LIST = "DELETE_LIST";
const UPDATE_LIST = "UPDATE_LIST";
const ADD_LIST_ITEM = "ADD_LIST_ITEM";
const UPDATE_LIST_ITEM = "UPDATE_LIST_ITEM";
const DELETE_LIST_ITEM = "DELETE_LIST_ITEM";

export const createListActions = <
	TList extends IListWithItems<TListItem>,
	TListItem extends IEntity
>() => {
	const actions = {
		setList: (list: TList, listName: string) =>
			createNamedAction(SET_LIST, listName, list),
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
	...createListActions(),
};

export const listReducer = <
	TState extends IListState<TList, TListItem>,
	TList extends IListWithItems<TListItem>,
	TListItem extends IEntity
>(
	state: TState,
	action: ListActions
): TState => {
	switch (action.type) {
		case "SET_LIST": {
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
};

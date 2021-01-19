import { IListWithItems } from "models";
import { IEntity } from "../../models/entities";
import { IListState } from "../../models/states";
import { updateListInState } from "../../utils";
import { ListActions } from "./lists.actions";

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

import {
	createTodoListReducer,
	ITodoListState,
	todoListInitialState,
} from "store/diaryLists";
import {
	ListCollectionHandler,
	ReducerCollection,
} from "../listCollectionHandler";
import { PurchaseListsActions } from "./purchaseLists.actions";
import { ListsStateByName } from "models/states";

export const purchaseListsHandler = new ListCollectionHandler(
	new ReducerCollection(
		todoListInitialState,
		createTodoListReducer,
		"purchaseLists"
	)
);

export const purchaseListsReducer = (
	state = {} as ListsStateByName<ITodoListState>,
	action: PurchaseListsActions
): ListsStateByName<ITodoListState> => {
	switch (action.type) {
		case "SET_PURCHASE_LISTS":
			return purchaseListsHandler.handleSetLists(action.payload);

		case "ADD_PURCHASE_LIST":
			return purchaseListsHandler.handleAddList(state, action.payload);

		case "DELETE_PURCHASE_LIST":
			return purchaseListsHandler.handleDeleteList(state, action.payload);

		default:
			return purchaseListsHandler.handleListAction(
				state,
				action.subjectName,
				(reducer, purchaseList) => reducer(purchaseList, action)
			);
	}
};

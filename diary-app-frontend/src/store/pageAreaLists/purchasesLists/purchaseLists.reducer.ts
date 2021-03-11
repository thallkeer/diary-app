import {
	createTodoListReducer,
	ITodoListState,
	todoListInitialState,
	TodoReducerType,
} from "store/diaryLists";
import { IPurchaseList } from "models/entities";
import {
	ListCollectionHandler,
	ReducerCollection,
} from "../listCollectionHandler";
import { PurchaseListsActions } from "./purchaseLists.actions";
import { ListsStateByName } from "models/states";

export interface IPurchaseListState {
	purchaseListId: number;
	purchaseAreaId: number;
	listState: ITodoListState;
}

class PurchaseListsReducerCollection extends ReducerCollection<
	TodoReducerType,
	IPurchaseList,
	IPurchaseListState
> {
	reducerNamePrefix = "purchaseLists";

	createReducer(listName: string) {
		return createTodoListReducer(listName);
	}

	createState(purchaseList: IPurchaseList): IPurchaseListState {
		const purchaseListState: IPurchaseListState = {
			purchaseListId: purchaseList.id,
			purchaseAreaId: purchaseList.areaOwnerId,
			listState: {
				...todoListInitialState,
				list: purchaseList.list,
				listName: "todoList_" + purchaseList.list.id,
			},
		};
		return purchaseListState;
	}
}

export const purchaseListsHandler = new ListCollectionHandler<
	IPurchaseListState,
	IPurchaseList,
	ITodoListState,
	TodoReducerType
>(new PurchaseListsReducerCollection());

export const purchaseListsReducer = (
	state = {} as ListsStateByName<IPurchaseListState>,
	action: PurchaseListsActions
): ListsStateByName<IPurchaseListState> => {
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
				(reducer, purchaseList) => reducer(purchaseList.listState, action)
			);
	}
};

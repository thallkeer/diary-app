import { ITodo, ITodoList } from "models";
import {
	createTodoListReducer,
	ITodoListState,
	todoListInitialState,
	todosReducer,
} from "store/diaryLists";
import { IPurchaseList } from "models/entities";
import { IDiaryListWrapperCollectionState } from "models/states";
import { ListWrapperUrls } from "models/types";
import { ListCollectionHandler } from "../listCollectionHandler";
import { PurchaseListsActions } from "./purchaseLists.actions";

const PURCHASE_LIST: ListWrapperUrls = "purchaseLists";

export interface IPurchaseListState {
	purchaseListId: number;
	purchaseAreaId: number;
	listState: ITodoListState;
}

export interface IPurchaseListsState
	extends IDiaryListWrapperCollectionState<IPurchaseListState> {}

const initialState: IPurchaseListsState = {
	byName: {},
};

type TodoReducerType = typeof todosReducer;
class PurchaseListsCollectionHandler extends ListCollectionHandler<
	IPurchaseListsState,
	IPurchaseListState,
	IPurchaseList,
	ITodoListState,
	ITodoList,
	ITodo,
	TodoReducerType
> {
	listNamePrefix = PURCHASE_LIST;

	createListReducer(listName: string) {
		return createTodoListReducer(listName);
	}

	listStateCreator(purchaseList: IPurchaseList): IPurchaseListState {
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

export const purchaseListsHandler = new PurchaseListsCollectionHandler();

export const purchaseListsReducer = (
	state = initialState,
	action: PurchaseListsActions
): IPurchaseListsState => {
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

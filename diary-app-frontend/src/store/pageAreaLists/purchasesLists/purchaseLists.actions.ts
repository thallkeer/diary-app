import { ActionsUnion, createAction } from "../../actions/action-helpers";
import { purchaseListsService } from "../../../services/concreteListService";
import { BaseThunkType } from "store/state.types";
import { ITodoList } from "models";
import { todosService } from "services/todosService";
import { TodoActions } from "store/diaryLists";

const SET_PURCHASE_LISTS = "SET_PURCHASE_LISTS";
const ADD_PURCHASE_LIST = "ADD_PURCHASE_LIST";
const DELETE_PURCHASE_LIST = "DELETE_PURCHASE_LIST";

const actions = {
	setLists: (purchaseLists: ITodoList[]) =>
		createAction(SET_PURCHASE_LISTS, purchaseLists),
	addPurchaseList: (purchaseList: ITodoList) =>
		createAction(ADD_PURCHASE_LIST, purchaseList),
	deletePurchaseList: (purchaseListId: number) =>
		createAction(DELETE_PURCHASE_LIST, purchaseListId),
};

export const purchaseListsThunks = {
	setPurchaseLists: (purchaseLists: ITodoList[]): ThunkType => async (
		dispatch
	) => {
		dispatch(actions.setLists(purchaseLists));
	},
	addPurchaseList: (
		purchaseList: ITodoList,
		purchasesAreaId: number
	): ThunkType => async (dispatch) => {
		const purchaseListId = await purchaseListsService.create(
			purchaseList,
			purchasesAreaId
		);
		const todoListId = await purchaseListsService.getTodoListId(purchaseListId);

		dispatch(
			actions.addPurchaseList({
				...purchaseList,
				id: todoListId,
			})
		);
	},
	deletePurchaseList: (purchaseListId: number): ThunkType => async (
		dispatch
	) => {
		await todosService.list.delete(purchaseListId);
		dispatch(actions.deletePurchaseList(purchaseListId));
	},
};

export type PurchaseListsActions = ActionsUnion<typeof actions> | TodoActions;
type ThunkType = BaseThunkType<PurchaseListsActions>;

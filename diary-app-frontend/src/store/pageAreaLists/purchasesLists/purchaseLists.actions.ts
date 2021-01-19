import { ActionsUnion, createAction } from "../../actions/action-helpers";
import { purchaseListsService } from "../../../services/concreteListService";
import { IPurchaseList } from "models/entities";
import {
	TodoActions,
	todoActions,
	todoThunks,
} from "store/diaryLists/todos.actions";
import { BaseThunkType } from "store/state.types";

const SET_PURCHASE_LISTS = "SET_PURCHASE_LISTS";
const ADD_PURCHASE_LIST = "ADD_PURCHASE_LIST";
const DELETE_PURCHASE_LIST = "DELETE_PURCHASE_LIST";

const actions = {
	setLists: (purchaseLists: IPurchaseList[]) =>
		createAction(SET_PURCHASE_LISTS, purchaseLists),
	addPurchaseList: (purchaseList: IPurchaseList) =>
		createAction(ADD_PURCHASE_LIST, purchaseList),
	deletePurchaseList: (purchaseListId: number) =>
		createAction(DELETE_PURCHASE_LIST, purchaseListId),
	...todoActions,
};

export const purchaseListsThunks = {
	...todoThunks,
	setPurchaseLists: (purchaseLists: IPurchaseList[]): ThunkType => async (
		dispatch
	) => {
		dispatch(actions.setLists(purchaseLists));
	},
	addPurchaseList: (purchaseList: IPurchaseList): ThunkType => async (
		dispatch
	) => {
		const id = await purchaseListsService.create(purchaseList);
		dispatch(
			actions.addPurchaseList({
				id: id,
				...purchaseList,
			})
		);
	},
	deletePurchaseList: (purchaseListId: number): ThunkType => async (
		dispatch
	) => {
		await purchaseListsService.delete(purchaseListId);
		dispatch(actions.deletePurchaseList(purchaseListId));
	},
};

export type PurchaseListsActions = ActionsUnion<typeof actions> | TodoActions;
type ThunkType = BaseThunkType<PurchaseListsActions>;

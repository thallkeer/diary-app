import { ActionsUnion, createAction } from "./action-helpers";
import axios from "../../axios/axios";
import { ITodoList, IPurchasesArea } from "../../models";
import { getPageArea } from "./monthPage-actions";
import { PageAreaBaseActionsGen } from "./pageArea-actions";

const ADD_PURCHASES_LIST = "ADD_PURCHASES_LIST";
const DELETE_PURCHASES_LIST = "DELETE_PURCHASES_LIST";

const purcshasesAreaBaseActions = PageAreaBaseActionsGen<IPurchasesArea>();
export const purcshasesAreaActions = {
	...purcshasesAreaBaseActions,
	addList: (purchasesList: ITodoList) =>
		createAction(ADD_PURCHASES_LIST, purchasesList),
	deleteList: (purchasesListId: number) =>
		createAction(DELETE_PURCHASES_LIST, purchasesListId),
};

export async function loadPurchasesArea(
	pageID: number,
	dispatch: React.Dispatch<PurchasesAreaActions>
) {
	dispatch(purcshasesAreaActions.setLoading(true));
	await getPageArea<IPurchasesArea>("purchasesArea", pageID).then((res) => {
		dispatch(purcshasesAreaActions.setArea(res.data));
		dispatch(purcshasesAreaActions.setLoading(false));
	});
}

export async function addPurchasesList(
	purchasesList: ITodoList,
	dispatch: React.Dispatch<PurchasesAreaActions>
) {
	if (!purchasesList) return;

	axios.post("todo", purchasesList).then((res) => {
		purchasesList.id = res.data;
		dispatch(purcshasesAreaActions.addList(purchasesList));
	});
}

export async function deletePurchasesList(
	purchasesList: ITodoList,
	dispatch: React.Dispatch<PurchasesAreaActions>
) {
	if (!purchasesList) return;
	dispatch(purcshasesAreaActions.deleteList(purchasesList.id));
}

export type PurchasesAreaActions = ActionsUnion<typeof purcshasesAreaActions>;

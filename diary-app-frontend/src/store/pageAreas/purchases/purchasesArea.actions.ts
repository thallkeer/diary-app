import { IPurchasesArea } from "models/PageAreas/pageAreas";
import { ActionsUnion } from "store/actions/action-helpers";
import { purchaseListsThunks } from "store/pageAreaLists/purchasesLists/purchaseLists.actions";
import { BaseThunkType } from "store/state.types";
import {
	getPageAreaActions,
	loadPageArea,
	PageAreaActions,
} from "../pageAreas.actions";

export const loadPurchasesArea = (pageID: number): ThunkType => async (
	dispatch
) => {
	dispatch(
		loadPageArea<IPurchasesArea>(
			"purchasesArea",
			"monthPage",
			pageID,
			(purchasesArea) => {
				dispatch(
					purchaseListsThunks.setPurchaseLists(purchasesArea.purchasesLists)
				);
			}
		)
	);
};

const purchasesAreaActions = {
	...getPageAreaActions<IPurchasesArea>("purchasesArea"),
};

export type PurchasesAreaActions =
	| ActionsUnion<typeof purchasesAreaActions>
	| PageAreaActions;
type ThunkType = BaseThunkType<PurchasesAreaActions>;

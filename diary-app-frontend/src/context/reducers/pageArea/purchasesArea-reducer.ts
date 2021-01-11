import { IPurchasesArea } from "../../../models/entities";
import {
	createNamedWrapperPageAreaReducer,
	getPageAreaActions,
	loadPageArea,
	PageAreaActions,
} from "./pageArea-reducer";
import { ActionsUnion } from "../../actions/action-helpers";
import { BaseThunkType } from "../../store";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import { IPurchasesAreaState } from "../../../models/states";
import { combineReducers } from "redux";
import {
	purchaseListsActions,
	purchaseListsReducer,
} from "../pageAreaLists/purchaseLists-reducer";

const initialState: IPurchasesAreaState = {
	area: null,
	pageAreaName: "purchasesArea",
	...INITIAL_LOADABLE_STATE,
};

export const purchasesAreaReducer = combineReducers({
	area: createNamedWrapperPageAreaReducer(
		initialState,
		initialState.pageAreaName
	),
	purchaseLists: purchaseListsReducer,
});

export const loadPurchasesArea = (pageID: number): ThunkType => async (
	dispatch
) => {
	dispatch(
		loadPageArea<IPurchasesArea>(
			initialState.pageAreaName,
			"monthPage",
			pageID,
			(purchasesArea) => {
				dispatch(
					purchaseListsActions.setPurchaseLists(purchasesArea.purchasesLists)
				);
			}
		)
	);
};

const purchasesAreaActions = {
	...getPageAreaActions<IPurchasesArea>(initialState.pageAreaName),
};

export type PurchasesAreaActions =
	| ActionsUnion<typeof purchasesAreaActions>
	| PageAreaActions;
type ThunkType = BaseThunkType<PurchasesAreaActions>;

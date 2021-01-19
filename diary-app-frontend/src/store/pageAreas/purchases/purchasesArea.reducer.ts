import { IPurchasesArea } from "models/PageAreas/pageAreas";
import { combineReducers } from "redux";
import { purchaseListsReducer } from "store/pageAreaLists/purchasesLists/purchaseLists.reducer";
import { INITIAL_LOADABLE_STATE } from "store/utilities/loading-reducer";
import { createPageAreaReducer, IPageAreaState } from "../pageAreas.reducer";

export interface IPurchasesAreaState extends IPageAreaState<IPurchasesArea> {}

const initialState: IPurchasesAreaState = {
	area: null,
	pageAreaName: "purchasesArea",
	...INITIAL_LOADABLE_STATE,
};

export const purchasesAreaReducer = combineReducers({
	area: createPageAreaReducer(initialState, initialState.pageAreaName),
	purchaseLists: purchaseListsReducer,
});

import { combineReducers } from "@reduxjs/toolkit";
import { IMonthPage, IPurchasesArea } from "models";
import { purchaseListsReducer } from "store/pageAreaLists/purchaseLists.slice";
import { createPageAreaSlice } from "./pageAreas.reducer";

const { slice, loadPageArea } = createPageAreaSlice<IMonthPage, IPurchasesArea>(
	"purchasesArea",
	"monthPage"
);

export const loadPurchasesArea = loadPageArea;

export const purchasesAreaReducer = combineReducers({
	area: slice.reducer,
	purchaseLists: purchaseListsReducer,
});

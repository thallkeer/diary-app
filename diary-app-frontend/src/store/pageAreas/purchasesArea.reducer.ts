import { combineReducers } from "@reduxjs/toolkit";
import { IMonthPage, IPurchasesArea } from "models";
import {
	purchaseListsReducer,
	setPurchaseLists,
} from "store/pageAreaLists/purchaseLists.slice";
import { createPageAreaSlice } from "./pageAreas.reducer";

const { slice, loadPageArea } = createPageAreaSlice<IMonthPage, IPurchasesArea>(
	"purchasesArea",
	"monthPage",
	(area) => setPurchaseLists(area.purchasesLists)
);

export const loadPurchasesArea = loadPageArea;

export const purchasesAreaReducer = combineReducers({
	area: slice.reducer,
	purchaseLists: purchaseListsReducer,
});

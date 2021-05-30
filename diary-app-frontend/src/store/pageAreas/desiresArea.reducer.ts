import { createPageAreaSlice } from "./pageAreas.reducer";
import { combineReducers } from "@reduxjs/toolkit";
import { desiresListsReducer } from "store/pageAreaLists/desireLists.slice";
import { IDesiresArea, IMonthPage } from "models";

const desiresAreaSlice = createPageAreaSlice<IMonthPage, IDesiresArea>(
	"desiresArea",
	"monthPage"
);

export const desiresAreaReducer = combineReducers({
	area: desiresAreaSlice.slice.reducer,
	desireLists: desiresListsReducer,
});

export const loadDesiresArea = desiresAreaSlice.loadPageArea;

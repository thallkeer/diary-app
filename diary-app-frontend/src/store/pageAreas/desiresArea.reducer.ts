import { createPageAreaSlice } from "./pageAreas.reducer";
import { combineReducers } from "@reduxjs/toolkit";
import {
	desiresListsReducer,
	setDesiresLists,
} from "store/pageAreaLists/desireLists.slice";
import { IDesiresArea, IMonthPage } from "models";

const desiresAreaSlice = createPageAreaSlice<IMonthPage, IDesiresArea>(
	"desiresArea",
	"monthPage",
	(area) => setDesiresLists(area.desiresLists)
);

export const desiresAreaReducer = combineReducers({
	area: desiresAreaSlice.slice.reducer,
	desireLists: desiresListsReducer,
});

export const loadDesiresArea = desiresAreaSlice.loadPageArea;

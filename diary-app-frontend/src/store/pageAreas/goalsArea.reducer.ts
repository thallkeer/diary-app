import { combineReducers } from "@reduxjs/toolkit";
import { IGoalsArea, IMonthPage } from "models";
import { goalListsReducer } from "store/pageAreaLists/goalLists.slice";

import { createPageAreaSlice } from "./pageAreas.reducer";

const goalsAreaSlice = createPageAreaSlice<IMonthPage, IGoalsArea>(
	"goalsArea",
	"monthPage"
);

export const goalsAreaReducer = combineReducers({
	area: goalsAreaSlice.slice.reducer,
	goalsLists: goalListsReducer,
});

export const loadGoalsArea = goalsAreaSlice.loadPageArea;

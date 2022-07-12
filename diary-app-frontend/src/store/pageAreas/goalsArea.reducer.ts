import { combineReducers } from "@reduxjs/toolkit";
import { IGoalsArea, IMonthPage } from "models";
import {
	goalListsReducer,
	setGoalLists,
} from "store/pageAreaLists/goalLists.slice";

import { createPageAreaSlice } from "./pageAreas.reducer";

const goalsAreaSlice = createPageAreaSlice<IMonthPage, IGoalsArea>(
	"goalsArea",
	"monthPage",
	(area) => setGoalLists(area.goalLists)
);

export const goalsAreaReducer = combineReducers({
	area: goalsAreaSlice.slice.reducer,
	goalsLists: goalListsReducer,
});

export const loadGoalsArea = goalsAreaSlice.loadPageArea;

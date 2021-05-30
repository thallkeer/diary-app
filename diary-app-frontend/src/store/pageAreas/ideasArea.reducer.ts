import { IIdeasArea } from "models/PageAreas/pageAreas";
import { ListWrapperUrls } from "models/types";
import { createPageAreaSlice } from "./pageAreas.reducer";
import { combineReducers } from "@reduxjs/toolkit";
import {
	createCommonListThunks,
	createCommonListSlice,
} from "store/diaryLists";
import { IMonthPage } from "models";

export const IDEAS_LIST: ListWrapperUrls = "ideasLists";

const ideasAreaSlice = createPageAreaSlice<IMonthPage, IIdeasArea>(
	"ideasArea",
	"monthPage"
);

const ideasListSlice = createCommonListSlice(IDEAS_LIST);
export const ideasListThunks = createCommonListThunks(ideasListSlice);

export const ideasAreaReducer = combineReducers({
	area: ideasAreaSlice.slice.reducer,
	ideasList: ideasListSlice.reducer,
});

export const loadIdeasArea = ideasAreaSlice.loadPageArea;

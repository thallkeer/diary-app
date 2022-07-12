import { IImportantThingsArea } from "models/PageAreas/pageAreas";
import { combineReducers } from "@reduxjs/toolkit";
import { createTodoSlice, createTodoListThunks } from "store/diaryLists";
import { createPageAreaSlice } from "./pageAreas.reducer";
import { IMainPage } from "models";

export const IMPORTANT_THINGS_LIST = "importantThingsList";

const importantThingsSlice = createTodoSlice(IMPORTANT_THINGS_LIST);
export const importantThingsThunks = createTodoListThunks(importantThingsSlice);

const setList = (area: IImportantThingsArea) =>
	importantThingsThunks.setList(area.importantThings);

export const importantThingsAreaSlice = createPageAreaSlice<
	IMainPage,
	IImportantThingsArea
>("importantThingsArea", "mainPage", setList);

export const importantThingsAreaReducer = combineReducers({
	area: importantThingsAreaSlice.slice.reducer,
	importantThingsList: importantThingsSlice.reducer,
});

export const loadImportantThingsArea = importantThingsAreaSlice.loadPageArea;

import { combineReducers } from "@reduxjs/toolkit";
import { IImportantEventsArea, IMainPage } from "models";
import { createEventListSlice, createEventListThunks } from "store/diaryLists";
import { createPageAreaSlice } from "./pageAreas.reducer";

export const IMPORTANT_EVENTS_LIST = "importantEventsList";

export const { slice, loadPageArea } = createPageAreaSlice<
	IMainPage,
	IImportantEventsArea
>("importantEventsArea", "mainPage");

const importantEventsSlice = createEventListSlice(IMPORTANT_EVENTS_LIST);

export const importantEventsThunks =
	createEventListThunks(importantEventsSlice);

export const importantEventsAreaReducer = combineReducers({
	area: slice.reducer,
	importantEventsList: importantEventsSlice.reducer,
});

export const loadImportantEventsArea = loadPageArea;

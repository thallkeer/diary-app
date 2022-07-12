import { combineReducers } from "@reduxjs/toolkit";
import { IImportantEventsArea, IMainPage } from "models";
import { createEventListSlice, createEventListThunks } from "store/diaryLists";
import { createPageAreaSlice } from "./pageAreas.reducer";

export const IMPORTANT_EVENTS_LIST = "importantEventsList";

const importantEventsSlice = createEventListSlice(IMPORTANT_EVENTS_LIST);

export const importantEventsThunks =
	createEventListThunks(importantEventsSlice);

const setList = (area: IImportantEventsArea) =>
	importantEventsThunks.setList(area.importantEvents);

export const { slice, loadPageArea } = createPageAreaSlice<
	IMainPage,
	IImportantEventsArea
>("importantEventsArea", "mainPage", setList);

export const importantEventsAreaReducer = combineReducers({
	area: slice.reducer,
	importantEventsList: importantEventsSlice.reducer,
});

export const loadImportantEventsArea = loadPageArea;

import { createSelector } from "reselect";
import { AppStateType } from "../context/store";

const getMainPageSelector = (state: AppStateType) => state.mainPage;

export const getMainPage = createSelector(
	getMainPageSelector,
	(mainPage) => mainPage.page
);

const getMonthPageSelector = (state: AppStateType) => state.monthPage;

export const getMonthPage = createSelector(
	getMonthPageSelector,
	(monthPage) => monthPage.page
);

export const getLoading = createSelector(
	getMainPageSelector,
	(mainPage) => mainPage.isLoading
);

export const getMainPageName = createSelector(
	getMainPageSelector,
	(mainPage) => mainPage.pageName
);

const getImportantThingsAreaSelector = (state: AppStateType) =>
	state.importantThingsArea;

export const getImportantThingsArea = createSelector(
	getImportantThingsAreaSelector,
	(importantThingsArea) => importantThingsArea
);

const getImportantThingsListSelector = (state: AppStateType) =>
	state.importantThingsArea.importantThingsList;

export const getImportantThingsList = createSelector(
	getImportantThingsListSelector,
	(importantThings) => {
		return importantThings;
	}
);

export const getImportantEventsListSelector = (state: AppStateType) =>
	state.importantEventsArea.importantEventsList;

export const getImportantEventsList = createSelector(
	getImportantEventsListSelector,
	(importantEvents) => importantEvents
);

const getImportantEventsAreaSelector = (state: AppStateType) =>
	state.importantEventsArea;

export const getImportantEventsArea = createSelector(
	getImportantEventsAreaSelector,
	(importantEvents) => importantEvents
);

const getPurchasesAreaSelector = (state: AppStateType) => state.purchasesArea;

export const getPurchasesArea = createSelector(
	getPurchasesAreaSelector,
	(purchasesArea) => purchasesArea
);

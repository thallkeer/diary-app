import { createSelector } from "reselect";
import { AppStateType } from "../context/store";

const getMainPageSelector = (state: AppStateType) => {
	return state.mainPage;
};
export const getMainPage = createSelector(getMainPageSelector, (mainPage) => {
	return mainPage.page;
});

export const getLoading = createSelector(getMainPageSelector, (mainPage) => {
	return mainPage.isLoading;
});

export const getMainPageName = createSelector(
	getMainPageSelector,
	(mainPage) => {
		return mainPage.pageName;
	}
);

const getImportantThingsAreaSelector = (state: AppStateType) => {
	return state.importantThingsArea;
};

export const getImportantThingsArea = createSelector(
	getImportantThingsAreaSelector,
	(importantThingsArea) => {
		return importantThingsArea;
	}
);

const getImportantThingsListSelector = (state: AppStateType) => {
	return state.importantThingsList;
};

export const getImportantThingsList = createSelector(
	getImportantThingsListSelector,
	(importantThings) => {
		return importantThings;
	}
);

export const getImportantEventsListSelector = (state: AppStateType) => {
	return state.importantEventsList;
};

export const getImportantEventsList = createSelector(
	getImportantEventsListSelector,
	(importantEvents) => {
		return importantEvents;
	}
);

const getImportantEventsAreaSelector = (state: AppStateType) => {
	return state.importantEventsArea;
};

export const getImportantEventsArea = createSelector(
	getImportantEventsAreaSelector,
	(importantEvents) => importantEvents
);

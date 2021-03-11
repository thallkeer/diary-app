import { AppStateType } from "store/reducer";
import { ListsStateByName } from "models/states";
import { createSelector } from "reselect";

export const getMainPage = (state: AppStateType) =>
	state.mainPage.pageState.page;

export const getMonthPage = (state: AppStateType) => state.monthPage.page.page;

export const getMainPageName = (state: AppStateType) =>
	state.mainPage.pageState.pageName;

export const getImportantThingsArea = (state: AppStateType) =>
	state.mainPage.importantThingsAreaState;

export const getImportantEventsArea = (state: AppStateType) =>
	state.mainPage.importantEventsAreaState;

export const getImportantThingsList = (state: AppStateType) =>
	state.mainPage.importantThingsAreaState.importantThingsList;

export const getImportantEventsList = (state: AppStateType) =>
	state.mainPage.importantEventsAreaState.importantEventsList;

export const getPurchasesArea = (state: AppStateType) =>
	state.monthPage.purchasesArea.area;

export const getPurchaseLists = (state: AppStateType) => {
	return Object.values(state.monthPage.purchasesArea.purchaseLists);
};

export const getDesiresArea = (state: AppStateType) =>
	state.monthPage.desiresArea.area;

export const getIdeasArea = (state: AppStateType) =>
	state.monthPage.ideasArea.area;

export const getGoalsArea = (state: AppStateType) => state.goalsArea.area;

export const getGoalsLists = (state: AppStateType) => {
	return Object.values(state.goalsArea.goalsLists);
};

export const getIdeasList = (state: AppStateType) => {
	return state.monthPage.ideasArea.ideasList;
};

export const getDesireLists = (state: AppStateType) => {
	return Object.values(state.monthPage.desiresArea.desireLists);
};

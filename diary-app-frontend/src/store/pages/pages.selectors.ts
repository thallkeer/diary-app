import { AppStateType } from "store/reducer";

export const getMainPage = (state: AppStateType) =>
	state.mainPage.pageState.page;

export const getMonthPage = (state: AppStateType) =>
	state.monthPage.pageState.page;

export const getMainPageName = (state: AppStateType) =>
	state.mainPage.pageState.pageName;

export const getImportantThingsArea = (state: AppStateType) =>
	state.mainPage.importantThingsArea.area;

export const getImportantEventsArea = (state: AppStateType) =>
	state.mainPage.importantEventsArea.area;

export const getImportantThingsList = (state: AppStateType) =>
	state.mainPage.importantThingsArea.importantThingsList;

export const getImportantEventsList = (state: AppStateType) => {
	return state.mainPage.importantEventsArea.importantEventsList;
};

export const getPurchasesArea = (state: AppStateType) =>
	state.monthPage.purchasesArea.area;

export const getPurchaseLists = (state: AppStateType) => {
	return Object.values(state.monthPage.purchasesArea.purchaseLists);
};

export const getDesiresArea = (state: AppStateType) =>
	state.monthPage.desiresArea.area;

export const getIdeasArea = (state: AppStateType) =>
	state.monthPage.ideasArea.area;

export const getGoalsArea = (state: AppStateType) =>
	state.monthPage.goalsArea.area;

export const getGoalsLists = (state: AppStateType) => {
	return Object.values(state.monthPage.goalsArea.goalsLists);
};

export const getIdeasList = (state: AppStateType) => {
	return state.monthPage.ideasArea.ideasList;
};

export const getDesireLists = (state: AppStateType) => {
	return Object.values(state.monthPage.desiresArea.desireLists);
};

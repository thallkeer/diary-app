import { createSelector } from "reselect";
import { AppStateType } from "../context/store";
import {
	IDesireListsState,
	IGoalsListsState,
	IPurchaseListsState,
} from "../models/states";

export const getMainPage = (state: AppStateType) => state.mainPage.page.page;

export const getMonthPage = (state: AppStateType) => state.monthPage.page.page;

export const getMainPageName = (state: AppStateType) =>
	state.mainPage.page.pageName;

export const getImportantThingsArea = (state: AppStateType) =>
	state.mainPage.importantThingsArea;

export const getImportantEventsArea = (state: AppStateType) =>
	state.mainPage.importantEventsArea;

export const getImportantThingsList = (state: AppStateType) =>
	state.mainPage.importantThingsArea.importantThingsList;

export const getImportantEventsList = (state: AppStateType) =>
	state.mainPage.importantEventsArea.importantEventsList;

export const getPurchasesArea = (state: AppStateType) =>
	state.monthPage.purchasesArea.area;

export const getPurchaseLists = (state: AppStateType) => {
	return Object.values(state.monthPage.purchasesArea.purchaseLists.byName);
};

export const getDesiresArea = (state: AppStateType) =>
	state.monthPage.desiresArea.area;

export const getIdeasArea = (state: AppStateType) =>
	state.monthPage.ideasArea.area;

export const getGoalsArea = (state: AppStateType) =>
	state.monthPage.goalsArea.area;

export const getGoalsLists = (state: AppStateType) => {
	return Object.values(state.monthPage.goalsArea.goalsLists.byName);
};

export const getIdeasList = (state: AppStateType) => {
	return state.monthPage.ideasArea.ideasList;
};

export const getDesireLists = (state: AppStateType) => {
	return Object.values(state.monthPage.desiresArea.desireLists.byName);
};

const getPurchaseListsSelector = (state: IPurchaseListsState) => {
	return state.byName;
};

export const getPurchaseListByName = (listName: string) =>
	createSelector(
		[getPurchaseListsSelector],
		(listsByName) => listsByName[listName]
	);

const getDesireListsSelector = (state: IDesireListsState) => {
	return state.byName;
};

export const getDesireListByName = (listName: string) =>
	createSelector(
		[getDesireListsSelector],
		(listsByName) => listsByName[listName]
	);

const getGoalsListsSelector = (state: IGoalsListsState) => {
	return state.byName;
};

export const getGoalListByName = (listName: string) =>
	createSelector(
		[getGoalsListsSelector],
		(listsByName) => listsByName[listName]
	);

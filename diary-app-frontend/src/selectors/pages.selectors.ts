import { useAppSelector } from "hooks/hooks";
import { createSelector } from "reselect";
import { RootState } from "store/store";

const selectMainPage = (state: RootState) => state.mainPage;
const selectMonthPage = (state: RootState) => state.monthPage;

const selectMainPageState = (state: RootState) => state.mainPage.pageState;
const selectMonthPageState = (state: RootState) => state.monthPage.pageState;

export const getMainPage = createSelector([selectMainPageState], (mp) => mp);

export const getImportantThingsArea = (state: RootState) =>
	selectMainPage(state).importantThingsArea.area;

export const getImportantEventsArea = createSelector(
	[selectMainPage],
	(mp) => mp.importantEventsArea.area
);

export const getImportantThingsList = createSelector(
	[selectMainPage],
	(mp) => mp.importantThingsArea.importantThingsList
);

export const getImportantEventsList = createSelector(
	[selectMainPage],
	(mp) => mp.importantEventsArea.importantEventsList
);

export const getMonthPage = createSelector([selectMonthPageState], (mp) => mp);
export const getPurchasesArea = createSelector(
	[selectMonthPage],
	(mp) => mp.purchasesArea.area
);

export const getPurchasesAreaState = createSelector(
	[selectMonthPage],
	(mp) => mp.purchasesArea
);

export const getDesiresArea = (s: RootState) => s.monthPage.desiresArea.area;

export const getDesiresAreaState = createSelector(
	[selectMonthPage],
	(mp) => mp.desiresArea
);

export const getIdeasArea = createSelector(
	[selectMonthPage],
	(mp) => mp.ideasArea.area
);

export const getIdeasAreaState = createSelector(
	[selectMonthPage],
	(mp) => mp.ideasArea
);

export const getGoalsArea = createSelector(
	[selectMonthPage],
	(mp) => mp.goalsArea.area
);

export const getGoalsAreaState = createSelector(
	[selectMonthPage],
	(mp) => mp.goalsArea
);

export const getGoalsLists = (state: RootState) => {
	return Object.values(getGoalsAreaState(state).goalsLists.entities);
};

export const getIdeasList = createSelector(
	[getIdeasAreaState],
	(mp) => mp.ideasList
);

export const getPurchaseLists = (state: RootState) => {
	return Object.values(getPurchasesAreaState(state).purchaseLists.entities);
};

export const getDesireLists = (state: RootState) => {
	return Object.values(getDesiresAreaState(state).desireLists.entities);
};

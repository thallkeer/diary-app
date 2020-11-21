import { createSelector } from "reselect";
import { AppStateType } from "../context/store";

const getAppSelector = (state: AppStateType) => {
	return state.app;
};
export const getAppInfo = createSelector(getAppSelector, (app) => {
	const { user, year, month } = app;
	return { user, year, month };
});

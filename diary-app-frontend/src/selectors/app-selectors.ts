import { createSelector } from "reselect";
import { AppStateType } from "../context/store";
import { IUser } from "../models";

const getAppSelector = (state: AppStateType) => state.app;

export type AppInfo = {
	year: number;
	month: number;
	user: IUser;
};

export const getAppInfo = createSelector(getAppSelector, (app) => {
	const appInfo: AppInfo = app;
	return appInfo;
});

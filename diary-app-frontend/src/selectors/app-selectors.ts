import { createSelector } from "reselect";
import { AppStateType } from "store/reducer";
import { IUser } from "../models/entities";

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

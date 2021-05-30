import { createSelector } from "reselect";
import { RootState } from "store/store";
import { IUser } from "../models/entities";

const getAppSelector = (state: RootState) => state.app;

export type AppInfo = {
	year: number;
	month: number;
	user: IUser;
};

export const getAppInfo = createSelector(getAppSelector, (app) => {
	const appInfo: AppInfo = app;
	return appInfo;
});

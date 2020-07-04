import { ActionsUnion, createAction } from "./action-helpers";
import { IMainPage, IMonthPage } from "../../models";
import { IAppState, PageType } from "..";

export const SET_APP_STATE = "SET_APP_STATE";
export const SET_MAIN_PAGE = "SET_MAIN_PAGE";
export const SET_MONTH_PAGE = "SET_MONTH_PAGE";
export const SET_SELECTED_PAGE = "SET_SELECTED_PAGE";

export const Actions = {
	setState: (state: IAppState) => createAction(SET_APP_STATE, state),
	setMainPage: (mainPage: IMainPage) => createAction(SET_MAIN_PAGE, mainPage),
	setMonthPage: (monthPage: IMonthPage) =>
		createAction(SET_MONTH_PAGE, monthPage),
	setSelectedPage: (page: PageType) => createAction(SET_SELECTED_PAGE, page),
};

export type AppActions = ActionsUnion<typeof Actions>;

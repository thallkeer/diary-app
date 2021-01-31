import { IUser } from "models/entities";
import { IPage } from "models/Pages/pages";
import { PageNames } from "models/types";
import { pageAPI } from "services/pageService";
import { ActionsUnion, createNamedAction } from "store/actions/action-helpers";
import { BaseThunkType } from "store/state.types";

export const LOAD_PAGE_START = "LOAD_PAGE_START";
export const LOAD_PAGE_SUCCESS = "LOAD_PAGE_SUCCESS";
export const LOAD_PAGE_ERROR = "LOAD_PAGE_ERROR";

const actions = {
	loadPageStart: (pageName: string) =>
		createNamedAction(LOAD_PAGE_START, pageName, undefined),
	loadPageSuccess: <TPage extends IPage>(page: TPage, pageName: string) =>
		createNamedAction(LOAD_PAGE_SUCCESS, pageName, page),
	loadPageError: <TPage extends IPage>(page: TPage, pageName: string) =>
		createNamedAction(LOAD_PAGE_ERROR, pageName, page),
};

export const Thunks = {
	loadPage: <TPage extends IPage>(
		pageName: PageNames,
		user: IUser,
		year: number,
		month: number
	): PageThunkType => async (dispatch) => {
		dispatch(actions.loadPageStart(pageName));
		const response = await pageAPI.getOrCreatePage<TPage>(
			pageName,
			user.id,
			year,
			month
		);
		dispatch(actions.loadPageSuccess<TPage>(response, pageName));
	},
};
export type PageActions = ActionsUnion<typeof actions>;
export type PageThunkType = BaseThunkType<PageActions>;

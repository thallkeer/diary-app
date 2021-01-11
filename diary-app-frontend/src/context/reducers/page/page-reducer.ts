import { ActionsUnion, createNamedAction } from "../../actions/action-helpers";
import { IPage, IPageArea, IUser } from "../../../models/entities";
import axios from "../../../axios/axios";
import { BaseThunkType } from "../../store";
import withLoadingStates from "../utilities/loading-reducer";
import { createNamedWrapperReducer } from "../../../utils";
import { IPageState } from "../../../models/states";
import { PageAreaNames, PageNames } from "../../../models/types";

export const LOAD_PAGE_START = "LOAD_PAGE_START";
export const LOAD_PAGE_SUCCESS = "LOAD_PAGE_SUCCESS";
export const LOAD_PAGE_ERROR = "LOAD_PAGE_ERROR";

export const createNamedWrapperPageReducer = <TPage extends IPage>(
	initialState: IPageState<TPage>,
	reducerName: PageNames
) => {
	return createNamedWrapperReducer(wrappedReducer, initialState, reducerName);
};

export const wrappedReducer = withLoadingStates({
	START: LOAD_PAGE_START,
	SUCCESS: LOAD_PAGE_SUCCESS,
	ERROR: LOAD_PAGE_ERROR,
})(pageReducer);

function pageReducer<TPage extends IPage>(
	state: IPageState<TPage>,
	action: PageActions
): IPageState<TPage> {
	switch (action.type) {
		case LOAD_PAGE_SUCCESS:
			return { ...state, page: action.payload as TPage };

		default:
			return state;
	}
}

const actions = {
	loadPageStart: (pageName: string) =>
		createNamedAction(LOAD_PAGE_START, pageName, undefined),
	loadPageSuccess: <TPage extends IPage>(page: TPage, pageName: string) =>
		createNamedAction(LOAD_PAGE_SUCCESS, pageName, page),
	loadPageError: <TPage extends IPage>(page: TPage, pageName: string) =>
		createNamedAction(LOAD_PAGE_ERROR, pageName, page),
};

export const loadPage = <TPage extends IPage>(
	pageName: PageNames,
	user: IUser,
	year: number,
	month: number
): PageThunkType => async (dispatch) => {
	dispatch(actions.loadPageStart(pageName));
	const response = await pageAPI.getPage<TPage>(pageName, user.id, year, month);
	dispatch(actions.loadPageSuccess<TPage>(response, pageName));
};

export const pageAPI = {
	async getPage<TPage extends IPage>(
		pageName: PageNames,
		userId: number,
		year: number,
		month: number
	) {
		const query = `${pageName}/${userId}/${year}/${month}`;
		const res = await axios.get<TPage>(query);
		return res.data;
	},
	async getPageArea<TArea extends IPageArea>(
		areaName: PageAreaNames,
		pageName: string,
		pageID: number
	) {
		const res = await axios.get<TArea>(`${pageName}/${areaName}/${pageID}`);
		return res.data;
	},
};

export type PageActions = ActionsUnion<typeof actions>;
export type PageThunkType = BaseThunkType<PageActions>;

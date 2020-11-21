import { ActionsUnion, createNamedAction } from "../../actions/action-helpers";
import { IPage, IPageArea, IPageState, IUser } from "../../../models";
import axios from "../../../axios/axios";
import { BaseThunkType, createNamedWrapperReducer } from "../../store";
import withLoadingStates from "../utilities/loading-reducer";

export const LOAD_PAGE_START = "PAGE/LOAD_PAGE_START";
export const LOAD_PAGE_SUCCESS = "PAGE/LOAD_PAGE_SUCCESS";
export const LOAD_PAGE_ERROR = "PAGE/LOAD_PAGE_ERROR";

export function createNamedWrapperPageReducer<TPage extends IPage>(
	initialState: IPageState<TPage>,
	reducerName: string
) {
	return createNamedWrapperReducer(
		wrappedReducer,
		initialState,
		reducerName,
		(action: PageActions) => action.subjectName
	);
}

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
		case "PAGE/LOAD_PAGE_SUCCESS":
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
	pageName: string,
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
		pageName: string,
		userId: number,
		year: number,
		month: number
	) {
		const query = `${pageName}/${userId}/${year}/${month}`;
		const res = await axios.get<TPage>(query);
		return res.data;
	},
	async getPageArea<TArea extends IPageArea>(
		areaName: string,
		pageName: string,
		pageID: number
	) {
		const res = await axios.get<TArea>(`${pageName}/${areaName}/${pageID}`);
		return res.data;
	},
};

export type PageActions = ActionsUnion<typeof actions>;
export type PageThunkType = BaseThunkType<PageActions>;

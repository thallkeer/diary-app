import { IPageAreaState, IPageArea } from "../../../models";
import { ActionsUnion, createNamedAction } from "../../actions/action-helpers";
import { BaseThunkType, createNamedWrapperReducer } from "../../store";
import { pageAPI } from "../page/page-reducer";
import withLoadingStates from "../utilities/loading-reducer";

const LOAD_PAGE_AREA_START = "PAGE_AREA/LOAD_PAGE_AREA_START";
const LOAD_PAGE_AREA_SUCCESS = "PAGE_AREA/LOAD_PAGE_AREA_SUCCESS";
const LOAD_PAGE_AREA_ERROR = "PAGE_AREA/LOAD_PAGE_AREA_ERROR";

export function createNamedWrapperPageAreaReducer<T extends IPageAreaState>(
	initialState: T,
	reducerName: string
) {
	return createNamedWrapperReducer(
		wrappedReducer,
		initialState,
		reducerName,
		(action: PageAreaActions) => action.subjectName
	);
}

export const wrappedReducer = withLoadingStates({
	START: LOAD_PAGE_AREA_START,
	SUCCESS: LOAD_PAGE_AREA_SUCCESS,
	ERROR: LOAD_PAGE_AREA_ERROR,
})(pageAreaReducer);

export function pageAreaReducer<
	T extends IPageArea,
	U extends IPageAreaState
>(state: U, action: PageAreaActions): U {
	switch (action.type) {
		case LOAD_PAGE_AREA_SUCCESS:
			return { ...state, area: action.payload as T };

		default:
			return state;
	}
}

const actions = {
	loadPageAreaStart: (areaName: string) =>
		createNamedAction(LOAD_PAGE_AREA_START, areaName, undefined),
	loadPageAreaSuccess: <T extends IPageArea>(area: T, areaName: string) =>
		createNamedAction(LOAD_PAGE_AREA_SUCCESS, areaName, area),
	loadPageAreaError: <T extends IPageArea>(area: T, areaName: string) =>
		createNamedAction(LOAD_PAGE_AREA_ERROR, areaName, area),
};

/**
 * 
 * @param areaName Название зоны страницы
 * @param pageName Название страницы
 * @param pageId Идентификатор страницы
 * @param onAreaLoaded Действие после загрузки данных
 */
export const loadPageArea = <TArea extends IPageArea>(
	areaName: string,
	pageName: string,
	pageId: number,
	onAreaLoaded: (pageArea: TArea) => void
): PageAreaThunkType => async (dispatch) => {
	dispatch(actions.loadPageAreaStart(areaName));
	const response = await pageAPI.getPageArea<TArea>(areaName, pageName, pageId);
	dispatch(actions.loadPageAreaSuccess(response, areaName));
	onAreaLoaded(response);
};

export type PageAreaActions = ActionsUnion<typeof actions>;
export type PageAreaThunkType = BaseThunkType<PageAreaActions>;

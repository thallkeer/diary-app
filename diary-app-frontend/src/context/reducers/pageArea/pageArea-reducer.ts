import { IPageArea } from "../../../models/entities";
import { IPageAreaState } from "../../../models/states";
import { PageAreaNames, PageNames } from "../../../models/types";
import { createNamedWrapperReducer } from "../../../utils";
import { ActionsUnion, createNamedAction } from "../../actions/action-helpers";
import { BaseThunkType } from "../../store";
import { pageAPI } from "../page/page-reducer";
import withLoadingStates from "../utilities/loading-reducer";

const LOAD_PAGE_AREA_START = "PAGE_AREA/LOAD_PAGE_AREA_START";
const LOAD_PAGE_AREA_SUCCESS = "PAGE_AREA/LOAD_PAGE_AREA_SUCCESS";
const LOAD_PAGE_AREA_ERROR = "PAGE_AREA/LOAD_PAGE_AREA_ERROR";

export function createNamedWrapperPageAreaReducer<
	TState extends IPageAreaState<TArea>,
	TArea extends IPageArea
>(initialState: TState, reducerName: string) {
	return createNamedWrapperReducer(wrappedReducer, initialState, reducerName);
}

export const wrappedReducer = withLoadingStates({
	START: LOAD_PAGE_AREA_START,
	SUCCESS: LOAD_PAGE_AREA_SUCCESS,
	ERROR: LOAD_PAGE_AREA_ERROR,
})(pageAreaReducer);

export function pageAreaReducer<
	T extends IPageArea,
	U extends IPageAreaState<T>
>(state: U, action: PageAreaActions): U {
	switch (action.type) {
		case LOAD_PAGE_AREA_SUCCESS:
			return { ...state, area: action.payload };

		default:
			return state;
	}
}

export const getPageAreaActions = <T extends IPageArea>(areaName: string) => {
	const actions = {
		loadPageAreaStart: () =>
			createNamedAction(LOAD_PAGE_AREA_START, areaName, undefined),
		loadPageAreaSuccess: (area: T) =>
			createNamedAction(LOAD_PAGE_AREA_SUCCESS, areaName, area),
		loadPageAreaError: (area: T) =>
			createNamedAction(LOAD_PAGE_AREA_ERROR, areaName, area),
	};
	return actions;
};

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
	areaName: PageAreaNames,
	pageName: PageNames,
	pageId: number,
	onAreaLoaded: (pageArea: TArea) => void
): PageAreaThunkType => async (dispatch) => {
	const actions = getPageAreaActions<TArea>(areaName);
	dispatch(actions.loadPageAreaStart());

	const response = await pageAPI.getPageArea<TArea>(areaName, pageName, pageId);

	dispatch(actions.loadPageAreaSuccess(response));

	onAreaLoaded(response);
};

export type PageAreaActions = ActionsUnion<typeof actions>;
export type PageAreaThunkType = BaseThunkType<PageAreaActions>;

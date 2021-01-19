import { IPageArea } from "models/PageAreas/pageAreas";
import { PageAreaNames, PageNames } from "models/types";
import { pageAPI } from "services/pageService";
import { ActionsUnion, createNamedAction } from "store/actions/action-helpers";
import { BaseThunkType } from "store/state.types";

export const LOAD_PAGE_AREA_START = "PAGE_AREA/LOAD_PAGE_AREA_START";
export const LOAD_PAGE_AREA_SUCCESS = "PAGE_AREA/LOAD_PAGE_AREA_SUCCESS";
export const LOAD_PAGE_AREA_ERROR = "PAGE_AREA/LOAD_PAGE_AREA_ERROR";

export const getPageAreaActions = <T extends IPageArea>(
	areaName: PageAreaNames
) => {
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

import { IPageArea } from "models/PageAreas/pageAreas";
import { IStateWithLoading } from "models/states";
import { PageAreaNames } from "models/types";
import { createNamedReducer } from "utils";
import withLoadingStates from "../utilities/loading-reducer";
import {
	LOAD_PAGE_AREA_START,
	LOAD_PAGE_AREA_SUCCESS,
	LOAD_PAGE_AREA_ERROR,
	PageAreaActions,
} from "store/pageAreas/pageAreas.actions";

export interface IPageAreaState<T extends IPageArea> extends IStateWithLoading {
	area: T;
	pageAreaName: PageAreaNames;
}

export function createPageAreaReducer<
	TState extends IPageAreaState<TArea>,
	TArea extends IPageArea
>(initialState: TState, reducerName: string) {
	return createNamedReducer(wrappedReducer, initialState, reducerName);
}

const wrappedReducer = withLoadingStates({
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

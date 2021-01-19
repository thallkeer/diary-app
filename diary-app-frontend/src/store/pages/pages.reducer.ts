import withLoadingStates from "store/utilities/loading-reducer";
import { IPage } from "models/Pages/pages";
import { IStateWithLoading } from "models/states";
import { PageNames } from "models/types";
import { createNamedReducer } from "utils";
import {
	LOAD_PAGE_START,
	LOAD_PAGE_SUCCESS,
	LOAD_PAGE_ERROR,
	PageActions,
} from "./pages.actions";

export interface IPageState<T extends IPage> extends IStateWithLoading {
	page: T;
	pageName: PageNames;
}

export const createPageReducer = <TPage extends IPage>(
	initialState: IPageState<TPage>,
	reducerName: PageNames
) => {
	return createNamedReducer(wrappedReducer, initialState, reducerName);
};

const wrappedReducer = withLoadingStates({
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

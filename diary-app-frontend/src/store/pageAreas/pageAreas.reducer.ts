import { IPage } from "models";
import { IPageArea } from "models/PageAreas/pageAreas";
import { IPageAreaState } from "models/states";
import { PageAreaUrls, PageUrls } from "models/types";
import { Dispatch } from "redux";
import { PageService } from "services/pageService";
import { ActionsUnion, createNamedAction } from "store/actions/action-helpers";
import { BaseThunkType } from "store/state.types";
import withLoadingStates from "store/utilities/loading-reducer";
import { createNamedReducer } from "utils";

export const LOAD_PAGE_AREA_START = "PAGE_AREA/LOAD_PAGE_AREA_START";
export const LOAD_PAGE_AREA_SUCCESS = "PAGE_AREA/LOAD_PAGE_AREA_SUCCESS";
export const LOAD_PAGE_AREA_ERROR = "PAGE_AREA/LOAD_PAGE_AREA_ERROR";

export abstract class PageAreaComponent<
	TPage extends IPage,
	TArea extends IPageArea
> {
	pageUrl: PageUrls;
	pageAreaUrl: PageAreaUrls;
	pageService: PageService<TPage>;

	constructor(pageUrl: PageUrls, pageAreaUrl: PageAreaUrls) {
		this.pageUrl = pageUrl;
		this.pageAreaUrl = pageAreaUrl;
		this.pageService = new PageService<TPage>(pageUrl);
	}

	private actions = {
		loadPageAreaStart: () =>
			createNamedAction(LOAD_PAGE_AREA_START, this.pageAreaUrl, undefined),
		loadPageAreaSuccess: (area: TArea) =>
			createNamedAction(LOAD_PAGE_AREA_SUCCESS, this.pageAreaUrl, area),
		loadPageAreaError: (area: TArea) =>
			createNamedAction(LOAD_PAGE_AREA_ERROR, this.pageAreaUrl, area),
	};

	public getActions() {
		return this.actions;
	}

	abstract onAreaLoaded(pageArea: TArea, dispatch: Dispatch): void;

	public loadPageArea(pageId: number) {
		const actions = this.getActions();
		type PageAreaActions = ActionsUnion<typeof actions>;
		type PageAreaThunkType = BaseThunkType<PageAreaActions>;

		const _loadPageArea = (): PageAreaThunkType => async (dispatch) => {
			dispatch(actions.loadPageAreaStart());

			const response = await this.pageService.getPageArea<TArea>(
				this.pageAreaUrl,
				pageId
			);

			dispatch(actions.loadPageAreaSuccess(response));

			this.onAreaLoaded(response, dispatch);
		};
		return _loadPageArea();
	}

	public getReducer<TState extends IPageAreaState<TArea>>(
		initialState: TState
	) {
		const actions = this.getActions();
		type TAreaActions = ActionsUnion<typeof actions>;
		const pageAreaReducer = (state: TState, action: TAreaActions): TState => {
			switch (action.type) {
				case LOAD_PAGE_AREA_SUCCESS:
					return { ...state, area: action.payload };

				default:
					return state;
			}
		};
		const wrappedReducer = withLoadingStates({
			START: LOAD_PAGE_AREA_START,
			SUCCESS: LOAD_PAGE_AREA_SUCCESS,
			ERROR: LOAD_PAGE_AREA_ERROR,
		})(pageAreaReducer);

		return createNamedReducer(
			wrappedReducer,
			initialState,
			initialState.pageAreaName
		);
	}
}

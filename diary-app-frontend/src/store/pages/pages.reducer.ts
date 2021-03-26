import { IUser } from "models/entities";
import { IPage } from "models/Pages/pages";
import { IPageState } from "models/states";
import { PageUrls } from "models/types";
import { PageService } from "services/pageService";
import { ActionsUnion, createNamedAction } from "store/actions/action-helpers";
import { BaseThunkType } from "store/state.types";
import withLoadingStates from "store/utilities/loading-reducer";
import { createNamedReducer } from "utils";

export const LOAD_PAGE_START = "LOAD_PAGE_START";
export const LOAD_PAGE_SUCCESS = "LOAD_PAGE_SUCCESS";
export const LOAD_PAGE_ERROR = "LOAD_PAGE_ERROR";

export abstract class PageComponent<TPage extends IPage> {
	pageUrl: PageUrls;
	pageService: PageService<TPage>;

	constructor(pageUrl: PageUrls) {
		this.pageUrl = pageUrl;
		this.pageService = new PageService<TPage>(pageUrl);
	}

	private actions = {
		loadPageStart: () =>
			createNamedAction(LOAD_PAGE_START, this.pageUrl, undefined),
		loadPageSuccess: (page: TPage) =>
			createNamedAction(LOAD_PAGE_SUCCESS, this.pageUrl, page),
		loadPageError: <TPage extends IPage>(page: TPage) =>
			createNamedAction(LOAD_PAGE_ERROR, this.pageUrl, page),
	};

	public getActions() {
		return this.actions;
	}

	public loadPage(user: IUser, year: number, month: number) {
		const actions = this.getActions();
		type PageActions = ActionsUnion<typeof actions>;
		type PageThunkType = BaseThunkType<PageActions>;

		const _loadPage = (): PageThunkType => async (dispatch) => {
			dispatch(actions.loadPageStart());
			const pageService = this.pageService;
			const response = await pageService.getOrCreatePage(user.id, year, month);
			dispatch(actions.loadPageSuccess(response));
		};

		return _loadPage();
	}

	public getReducer<TState extends IPageState<TPage>>(initialState: TState) {
		const actions = this.getActions();
		type TPageActions = ActionsUnion<typeof actions>;
		const pageReducer = (state: TState, action: TPageActions): TState => {
			switch (action.type) {
				case "LOAD_PAGE_SUCCESS":
					return { ...state, page: action.payload };

				default:
					return state;
			}
		};
		const wrappedReducer = withLoadingStates({
			START: LOAD_PAGE_START,
			SUCCESS: LOAD_PAGE_SUCCESS,
			ERROR: LOAD_PAGE_ERROR,
		})(pageReducer);

		return createNamedReducer(
			wrappedReducer,
			initialState,
			initialState.pageName
		);
	}
}

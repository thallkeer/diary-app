import { IPage, IPageState, IUser } from "../../../models";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import {
	createNamedWrapperPageReducer,
	loadPage,
	PageThunkType,
} from "./page-reducer";

export interface IMainPage extends IPage {}

export interface IMainPageState extends IPageState<IMainPage> {}

const MainPageName: string = "mainPage";

const initialState: IMainPageState = {
	page: null,
	pageName: MainPageName,
	...INITIAL_LOADABLE_STATE,
};

export const mainPageReducer = createNamedWrapperPageReducer<IMainPage>(
	initialState,
	MainPageName
);

export const loadMainPage = (
	user: IUser,
	year: number,
	month: number
): PageThunkType => async (dispatch) => {
	dispatch(loadPage<IMainPage>(MainPageName, user, year, month));
};

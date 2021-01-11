import { combineReducers } from "redux";
import { IPage, IUser } from "../../../models/entities";
import { IPageState } from "../../../models/states";
import { importantEventsAreaReducer } from "../pageArea/importantEventsArea-reducer";
import { importantThingsAreaReducer } from "../pageArea/importantThingsArea-reducer";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import {
	createNamedWrapperPageReducer,
	loadPage,
	PageThunkType,
} from "./page-reducer";
import { PageNames } from "../../../models/types";

export interface IMainPage extends IPage {}

export interface IMainPageState extends IPageState<IMainPage> {}

export const MainPageName: PageNames = "mainPage";

const initialState: IMainPageState = {
	page: null,
	pageName: MainPageName,
	...INITIAL_LOADABLE_STATE,
};

export const mainPageReducer = combineReducers({
	page: createNamedWrapperPageReducer<IMainPage>(initialState, MainPageName),
	importantThingsArea: importantThingsAreaReducer,
	importantEventsArea: importantEventsAreaReducer,
});

export const loadMainPage = (
	user: IUser,
	year: number,
	month: number
): PageThunkType => async (dispatch) => {
	dispatch(loadPage<IMainPage>(MainPageName, user, year, month));
};

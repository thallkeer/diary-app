import { combineReducers } from "redux";
import { importantEventsAreaReducer } from "../pageAreas/importantEvents/importantEventsArea.reducer";
import { importantThingsAreaReducer } from "../pageAreas/importantThings/importantThingsArea.reducer";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import { createPageReducer, IPageState } from "./pages.reducer";
import { IMainPage } from "models/Pages/pages";

export interface IMainPageState extends IPageState<IMainPage> {}

const initialState: IMainPageState = {
	page: null,
	pageName: "mainPage",
	...INITIAL_LOADABLE_STATE,
};

export const mainPageReducer = combineReducers({
	pageState: createPageReducer<IMainPage>(initialState, "mainPage"),
	importantThingsAreaState: importantThingsAreaReducer,
	importantEventsAreaState: importantEventsAreaReducer,
});

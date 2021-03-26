import { combineReducers } from "redux";
import { importantEventsAreaReducer } from "../pageAreas/importantEventsArea.reducer";
import { importantThingsAreaReducer } from "../pageAreas/importantThingsArea.reducer";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import { IMainPage } from "models/Pages/pages";
import { PageComponent } from "./pages.reducer";
import { IPageState } from "models/states";

class MainPageComponent extends PageComponent<IMainPage> {}

export interface IMainPageState extends IPageState<IMainPage> {}

const initialState: IMainPageState = {
	page: null,
	pageName: "mainPage",
	...INITIAL_LOADABLE_STATE,
};

export const mainPageComponent = new MainPageComponent("mainPage");

export const mainPageReducer = combineReducers({
	pageState: mainPageComponent.getReducer(initialState),
	importantThingsArea: importantThingsAreaReducer,
	importantEventsArea: importantEventsAreaReducer,
});

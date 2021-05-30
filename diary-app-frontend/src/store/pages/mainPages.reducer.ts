import { combineReducers } from "@reduxjs/toolkit";
import { importantEventsAreaReducer } from "../pageAreas/importantEventsArea.reducer";
import { importantThingsAreaReducer } from "../pageAreas/importantThingsArea.reducer";
import { INITIAL_LOADABLESTATE } from "../utilities/loading-reducer";
import { IMainPage } from "models/Pages/pages";
import { PageComponent } from "./pages.reducer";
import { IPageState } from "models/states";

export interface IMainPageState extends IPageState<IMainPage> {}

const initialState: IMainPageState = {
	page: null,
	pageName: "mainPage",
	...INITIAL_LOADABLESTATE,
};

export const mainPageComponent = new PageComponent("mainPage", initialState);

export const mainPageReducer = combineReducers({
	pageState: mainPageComponent.slice.reducer,
	importantThingsArea: importantThingsAreaReducer,
	importantEventsArea: importantEventsAreaReducer,
});

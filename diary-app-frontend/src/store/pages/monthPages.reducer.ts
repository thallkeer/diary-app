import { IMonthPage } from "models/Pages/pages";
import { IPageState } from "models/states";
import { combineReducers } from "@reduxjs/toolkit";
import { goalsAreaReducer } from "store/pageAreas";
import { desiresAreaReducer } from "store/pageAreas/desiresArea.reducer";
import { ideasAreaReducer } from "store/pageAreas/ideasArea.reducer";
import { purchasesAreaReducer } from "store/pageAreas/purchasesArea.reducer";
import { INITIAL_LOADABLESTATE } from "store/utilities/loading-reducer";
import { PageComponent } from "./pages.reducer";

export interface IMonthPageState extends IPageState<IMonthPage> {}

const initialState: IMonthPageState = {
	pageName: "monthPage",
	page: null,
	...INITIAL_LOADABLESTATE,
};

export const monthPageComponent = new PageComponent("monthPage", initialState);

export const monthPageReducer = combineReducers({
	pageState: monthPageComponent.slice.reducer,
	purchasesArea: purchasesAreaReducer,
	desiresArea: desiresAreaReducer,
	ideasArea: ideasAreaReducer,
	goalsArea: goalsAreaReducer,
});

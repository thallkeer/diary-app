import { IMonthPage } from "models/Pages/pages";
import { combineReducers } from "redux";
import { desiresAreaReducer } from "store/pageAreas/desires/desiresArea.reducer";
import { ideasAreaReducer } from "store/pageAreas/ideas/ideasArea.reducer";
import { purchasesAreaReducer } from "store/pageAreas/purchases/purchasesArea.reducer";
import { INITIAL_LOADABLE_STATE } from "store/utilities/loading-reducer";
import { createPageReducer, IPageState } from "./pages.reducer";

export interface IMonthPageState extends IPageState<IMonthPage> {}

const initialState: IMonthPageState = {
	pageName: "monthPage",
	page: null,
	...INITIAL_LOADABLE_STATE,
};

export const monthPageReducer = combineReducers({
	page: createPageReducer<IMonthPage>(initialState, "monthPage"),
	purchasesArea: purchasesAreaReducer,
	desiresArea: desiresAreaReducer,
	ideasArea: ideasAreaReducer,
});

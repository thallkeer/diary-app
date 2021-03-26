import { IMonthPage } from "models/Pages/pages";
import { IPageState } from "models/states";
import { combineReducers } from "redux";
import { goalsAreaReducer } from "store/pageAreas";
import { desiresAreaReducer } from "store/pageAreas/desiresArea.reducer";
import { ideasAreaReducer } from "store/pageAreas/ideasArea.reducer";
import { purchasesAreaReducer } from "store/pageAreas/purchasesArea.reducer";
import { INITIAL_LOADABLE_STATE } from "store/utilities/loading-reducer";
import { PageComponent } from "./pages.reducer";

class MonthPageComponent extends PageComponent<IMonthPage> {}
export const monthPageComponent = new MonthPageComponent("monthPage");

export interface IMonthPageState extends IPageState<IMonthPage> {}

const initialState: IMonthPageState = {
	pageName: "monthPage",
	page: null,
	...INITIAL_LOADABLE_STATE,
};

export const monthPageReducer = combineReducers({
	pageState: monthPageComponent.getReducer(initialState),
	purchasesArea: purchasesAreaReducer,
	desiresArea: desiresAreaReducer,
	ideasArea: ideasAreaReducer,
	goalsArea: goalsAreaReducer,
});

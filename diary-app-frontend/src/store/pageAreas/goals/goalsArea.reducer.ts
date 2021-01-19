import { combineReducers } from "redux";
import { createPageAreaReducer, IPageAreaState } from "../pageAreas.reducer";
import { INITIAL_LOADABLE_STATE } from "../../utilities/loading-reducer";
import { IGoalsArea } from "models/PageAreas/pageAreas";
import { goalsListsReducer } from "store/pageAreaLists/goalsLists/goalsLists.reducer";

export interface IGoalsAreaState extends IPageAreaState<IGoalsArea> {}

const initialState: IGoalsAreaState = {
	area: null,
	pageAreaName: "goalsArea",
	...INITIAL_LOADABLE_STATE,
};

export const goalsAreaReducer = combineReducers({
	area: createPageAreaReducer(initialState, initialState.pageAreaName),
	goalsLists: goalsListsReducer,
});

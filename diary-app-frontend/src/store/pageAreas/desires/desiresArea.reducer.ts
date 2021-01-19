import { INITIAL_LOADABLE_STATE } from "../../utilities/loading-reducer";
import { combineReducers } from "redux";
import { createPageAreaReducer, IPageAreaState } from "../pageAreas.reducer";
import { IDesiresArea } from "models/PageAreas/pageAreas";
import { desireListsReducer } from "store/pageAreaLists/desiresLists/desireLists.reducer";

export interface IDesiresAreaState extends IPageAreaState<IDesiresArea> {}

const initialState: IDesiresAreaState = {
	area: null,
	pageAreaName: "desiresArea",
	...INITIAL_LOADABLE_STATE,
};

export const desiresAreaReducer = combineReducers({
	area: createPageAreaReducer(initialState, initialState.pageAreaName),
	desireLists: desireListsReducer,
});

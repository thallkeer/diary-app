import { IImportantThingsArea } from "models/PageAreas/pageAreas";
import { combineReducers } from "redux";
import { createTodoListReducer } from "store/diaryLists/todos.reducer";
import { INITIAL_LOADABLE_STATE } from "../../utilities/loading-reducer";
import { createPageAreaReducer, IPageAreaState } from "../pageAreas.reducer";
import { IMPORTANT_THINGS_LIST } from "./importantThingsArea.actions";

export interface IImportantThingsAreaState
	extends IPageAreaState<IImportantThingsArea> {}

const initialState: IImportantThingsAreaState = {
	area: null,
	pageAreaName: "importantThingsArea",
	...INITIAL_LOADABLE_STATE,
};

export const importantThingsAreaReducer = combineReducers({
	area: createPageAreaReducer(initialState, initialState.pageAreaName),
	importantThingsList: createTodoListReducer(IMPORTANT_THINGS_LIST),
});

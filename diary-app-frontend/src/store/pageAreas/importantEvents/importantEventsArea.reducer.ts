import { IImportantEventsArea } from "models/PageAreas/pageAreas";
import { combineReducers } from "redux";
import { createEventListReducer } from "store/diaryLists/events.reducer";
import { INITIAL_LOADABLE_STATE } from "../../utilities/loading-reducer";
import { createPageAreaReducer, IPageAreaState } from "../pageAreas.reducer";
import { IMPORTANT_EVENTS_LIST } from "./importantEventsArea.actions";

export interface IImportantEventsAreaState
	extends IPageAreaState<IImportantEventsArea> {}

const initialState: IImportantEventsAreaState = {
	area: null,
	pageAreaName: "importantEventsArea",
	...INITIAL_LOADABLE_STATE,
};

export const importantEventsAreaReducer = combineReducers({
	area: createPageAreaReducer(initialState, initialState.pageAreaName),
	importantEventsList: createEventListReducer(IMPORTANT_EVENTS_LIST),
});

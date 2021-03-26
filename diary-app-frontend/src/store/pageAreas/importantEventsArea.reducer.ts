import { IMainPage } from "models";
import { IImportantEventsArea } from "models/PageAreas/pageAreas";
import { IPageAreaState } from "models/states";
import { combineReducers } from "redux";
import { createEventListReducer, eventListComponent } from "store/diaryLists";
import { INITIAL_LOADABLE_STATE } from "store/utilities/loading-reducer";
import { PageAreaComponent } from "./pageAreas.reducer";

export const IMPORTANT_EVENTS_LIST = "importantEventsList";

class ImportantEventsAreaComponent extends PageAreaComponent<
	IMainPage,
	IImportantEventsArea
> {
	onAreaLoaded(pageArea: IImportantEventsArea, dispatch): void {
		dispatch(importantEventsThunks.setList(pageArea.importantEvents));
	}
}

export const importantEventsThunks = eventListComponent.getThunks(
	IMPORTANT_EVENTS_LIST
);

export const importantEventsAreaComponent = new ImportantEventsAreaComponent(
	"mainPage",
	"importantEventsArea"
);

export interface IImportantEventsAreaState
	extends IPageAreaState<IImportantEventsArea> {}

const initialState: IImportantEventsAreaState = {
	area: null,
	pageAreaName: "importantEventsArea",
	...INITIAL_LOADABLE_STATE,
};

export const importantEventsAreaReducer = combineReducers({
	area: importantEventsAreaComponent.getReducer(initialState),
	importantEventsList: createEventListReducer(IMPORTANT_EVENTS_LIST),
});

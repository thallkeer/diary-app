import { IMainPage } from "models";
import { IImportantThingsArea } from "models/PageAreas/pageAreas";
import { IPageAreaState } from "models/states";
import { combineReducers } from "redux";
import { createTodoListReducer, todoListComponent } from "store/diaryLists";
import { INITIAL_LOADABLE_STATE } from "store/utilities/loading-reducer";
import { PageAreaComponent } from "./pageAreas.reducer";

export const IMPORTANT_THINGS_LIST = "importantThingsList";

class ImportantThingsAreaComponent extends PageAreaComponent<
	IMainPage,
	IImportantThingsArea
> {
	onAreaLoaded(pageArea: IImportantThingsArea, dispatch): void {
		dispatch(importantThingsThunks.setList(pageArea.importantThings));
	}
}

export const importantThingsThunks = todoListComponent.getThunks(
	IMPORTANT_THINGS_LIST
);

export const importantThingsAreaComponent = new ImportantThingsAreaComponent(
	"mainPage",
	"importantThingsArea"
);

export interface IImportantThingsAreaState
	extends IPageAreaState<IImportantThingsArea> {}

const initialState: IImportantThingsAreaState = {
	area: null,
	pageAreaName: "importantThingsArea",
	...INITIAL_LOADABLE_STATE,
};

export const importantThingsAreaReducer = combineReducers({
	area: importantThingsAreaComponent.getReducer(initialState),
	importantThingsList: createTodoListReducer(IMPORTANT_THINGS_LIST),
});

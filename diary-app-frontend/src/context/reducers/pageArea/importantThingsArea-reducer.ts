import { combineReducers } from "redux";
import { IImportantThingsArea } from "../../../models/entities";
import { IImportantThingsAreaState } from "../../../models/states";
import { ActionsUnion } from "../../actions/action-helpers";
import { BaseThunkType } from "../../store";
import { createTodoListReducer, todoActionCreators } from "../list/todos";
import { MainPageName } from "../page/mainPage-reducer";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import {
	createNamedWrapperPageAreaReducer,
	loadPageArea,
} from "./pageArea-reducer";

const initialState: IImportantThingsAreaState = {
	area: null,
	pageAreaName: "importantThingsArea",
	...INITIAL_LOADABLE_STATE,
};

export const IMPORTANT_THINGS_LIST = "importantThingsList";

export const importantThingsAreaReducer = combineReducers({
	area: createNamedWrapperPageAreaReducer(
		initialState,
		initialState.pageAreaName
	),
	importantThingsList: createTodoListReducer(IMPORTANT_THINGS_LIST),
});

const actions = {};

export const loadImportantThingsArea = (pageID: number): ThunkType => async (
	dispatch
) => {
	dispatch(
		loadPageArea<IImportantThingsArea>(
			initialState.pageAreaName,
			MainPageName,
			pageID,
			(pageArea) => {
				dispatch(
					todoActionCreators.setList(
						pageArea.importantThings,
						IMPORTANT_THINGS_LIST
					)
				);
			}
		)
	);
};

export type ImportantThingsAreaActions = ActionsUnion<typeof actions>;
type ThunkType = BaseThunkType<ImportantThingsAreaActions>;

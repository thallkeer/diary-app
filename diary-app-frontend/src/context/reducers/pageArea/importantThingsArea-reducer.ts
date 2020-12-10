import { combineReducers } from "redux";
import { IPageArea, IPageAreaState, ITodoList } from "../../../models";
import { ActionsUnion } from "../../actions/action-helpers";
import { BaseThunkType } from "../../store";
import { createTodoListReducer, todoActions } from "../list/todos";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import {
	createNamedWrapperPageAreaReducer,
	loadPageArea,
} from "./pageArea-reducer";

interface IImportantThingsArea extends IPageArea {
	importantThings: ITodoList;
}

export interface IImportantThingsAreaState
	extends IPageAreaState<IImportantThingsArea> {}

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
			"mainPage",
			pageID,
			(pageArea) => {
				dispatch(
					todoActions.setList(pageArea.importantThings, IMPORTANT_THINGS_LIST)
				);
			}
		)
	);
};

export type ImportantThingsAreaActions = ActionsUnion<typeof actions>;
type ThunkType = BaseThunkType<ImportantThingsAreaActions>;

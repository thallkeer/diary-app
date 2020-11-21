import {  IPageArea, IPageAreaState, ITodoList } from "../../../models";
import { ActionsUnion } from "../../actions/action-helpers";
import { BaseThunkType } from "../../store";
import { createTodoListReducer, todoActions } from "../list/todos";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import {
	createNamedWrapperPageAreaReducer,
	loadPageArea,
} from "./pageArea-reducer";

export interface IImportantThingsAreaState
	extends IPageAreaState {
	}

const initialState: IImportantThingsAreaState = {
	area: null,
	pageAreaName: "importantThingsArea",
	...INITIAL_LOADABLE_STATE,
};

export const importantThingsAreaReducer = createNamedWrapperPageAreaReducer(
	initialState,
	initialState.pageAreaName
);

export const importantThingsListReducer = createTodoListReducer('importantThingsList');

const actions = {};

interface IImportantThingsArea extends IPageArea {
	importantThings: ITodoList;
}

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
					todoActions.setList(pageArea.importantThings)
				);
			}
		)
	);
};

export type ImportantThingsAreaActions = ActionsUnion<typeof actions>;
type ThunkType = BaseThunkType<ImportantThingsAreaActions>;

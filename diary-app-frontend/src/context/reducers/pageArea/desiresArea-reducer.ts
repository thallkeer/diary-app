import { ActionsUnion } from "../../actions/action-helpers";
import { IDesiresArea } from "../../../models/entities";
import {
	createNamedWrapperPageAreaReducer,
	getPageAreaActions,
	loadPageArea,
	PageAreaActions,
} from "./pageArea-reducer";
import { BaseThunkType } from "../../store";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import { IDesiresAreaState } from "../../../models/states";
import { combineReducers } from "redux";
import {
	desireListsActions,
	desireListsReducer,
} from "../pageAreaLists/desireLists-reducer";

const initialState: IDesiresAreaState = {
	area: null,
	pageAreaName: "desiresArea",
	...INITIAL_LOADABLE_STATE,
};

export const desiresAreaReducer = combineReducers({
	area: createNamedWrapperPageAreaReducer(
		initialState,
		initialState.pageAreaName
	),
	desireLists: desireListsReducer,
});

export const loadDesiresArea = (pageID: number): ThunkType => async (
	dispatch
) => {
	dispatch(
		loadPageArea<IDesiresArea>(
			initialState.pageAreaName,
			"monthPage",
			pageID,
			(desiresArea) => {
				dispatch(desireListsActions.setDesireLists(desiresArea.desiresLists));
			}
		)
	);
};

const desiresAreaActions = {
	...getPageAreaActions<IDesiresArea>(initialState.pageAreaName),
};

export type DesiresAreaActions =
	| ActionsUnion<typeof desiresAreaActions>
	| PageAreaActions;
type ThunkType = BaseThunkType<DesiresAreaActions>;

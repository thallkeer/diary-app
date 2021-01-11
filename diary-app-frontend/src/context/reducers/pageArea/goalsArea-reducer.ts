import { IGoalsAreaState } from "../../../models/states";
import { combineReducers } from "redux";
import {
	createNamedWrapperPageAreaReducer,
	getPageAreaActions,
	loadPageArea,
	PageAreaActions,
} from "./pageArea-reducer";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import {
	goalsListsActions,
	goalsListsReducer,
} from "../pageAreaLists/goalLists-reducer";
import { IGoalsArea } from "../../../models/entities";
import { ActionsUnion } from "../../actions/action-helpers";
import { BaseThunkType } from "../../store";

const initialState: IGoalsAreaState = {
	area: null,
	pageAreaName: "goalsArea",
	...INITIAL_LOADABLE_STATE,
};

export const goalsAreaReducer = combineReducers({
	area: createNamedWrapperPageAreaReducer(
		initialState,
		initialState.pageAreaName
	),
	goalsLists: goalsListsReducer,
});

export const loadGoalsArea = (pageID: number): ThunkType => async (
	dispatch
) => {
	dispatch(
		loadPageArea<IGoalsArea>(
			initialState.pageAreaName,
			"monthPage",
			pageID,
			(goalsArea) => {
				dispatch(goalsListsActions.setGoalsLists(goalsArea.goalLists));
			}
		)
	);
};

const goalsAreaActions = {
	...getPageAreaActions<IGoalsArea>(initialState.pageAreaName),
};

export type PurchasesAreaActions =
	| ActionsUnion<typeof goalsAreaActions>
	| PageAreaActions;
type ThunkType = BaseThunkType<PurchasesAreaActions>;

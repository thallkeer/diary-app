import { IGoalList } from "models/entities";
import { habitTrackerService } from "services/concreteListService";
import { ActionsUnion, createAction } from "store/actions/action-helpers";
import {
	habitTrackerActions,
	habitTrackerThunks,
} from "store/diaryLists/habitTrackers.actions";
import { BaseThunkType } from "store/state.types";

export const SET_GOALS_LIST = "SET_GOALS_LIST";
export const ADD_GOALS_LIST = "ADD_GOALS_LIST";
export const DELETE_GOALS_LIST = "DELETE_GOALS_LIST";

const actions = {
	setGoalsLists: (trackers: IGoalList[]) =>
		createAction(SET_GOALS_LIST, trackers),
	addGoalsList: (tracker: IGoalList) => createAction(ADD_GOALS_LIST, tracker),
	deleteGoalsList: (trackerId: number) =>
		createAction(DELETE_GOALS_LIST, trackerId),
	...habitTrackerActions,
};

export const goalsListsThunks = {
	...habitTrackerThunks,
	setGoalsLists: (goalsLists: IGoalList[]): ThunkType => async (dispatch) => {
		dispatch(actions.setGoalsLists(goalsLists));
	},
	addGoalsList: (goalList: IGoalList): ThunkType => async (dispatch) => {
		if (!goalList) return;

		const id = await habitTrackerService.create(goalList.list);
		const newGoalList = {
			...goalList,
			id,
			list: {
				...goalList.list,
				id,
			},
		};
		dispatch(actions.addGoalsList(newGoalList));
	},
	deleteGoalsList: (trackerId: number): ThunkType => async (dispatch) => {
		await habitTrackerService.delete(trackerId);
		dispatch(actions.deleteGoalsList(trackerId));
	},
};

export type GoalsListsActions = ActionsUnion<typeof actions>;
type ThunkType = BaseThunkType<GoalsListsActions>;

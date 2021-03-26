import { IHabitTracker } from "models";
import { habitTrackerService } from "services/concreteListService";
import { ActionsUnion, createAction } from "store/actions/action-helpers";
import { HabitTrackerActions } from "store/diaryLists/habitTrackers.reducer";
import { BaseThunkType } from "store/state.types";

export const SET_GOALS_LIST = "SET_GOALS_LIST";
export const ADD_GOALS_LIST = "ADD_GOALS_LIST";
export const DELETE_GOALS_LIST = "DELETE_GOALS_LIST";

const actions = {
	setGoalsLists: (trackers: IHabitTracker[]) =>
		createAction(SET_GOALS_LIST, trackers),
	addGoalsList: (tracker: IHabitTracker) =>
		createAction(ADD_GOALS_LIST, tracker),
	deleteGoalsList: (trackerId: number) =>
		createAction(DELETE_GOALS_LIST, trackerId),
};

export const goalsListsThunks = {
	setGoalsLists: (goalsLists: IHabitTracker[]): ThunkType => async (
		dispatch
	) => {
		dispatch(actions.setGoalsLists(goalsLists));
	},
	addGoalsList: (goalList: IHabitTracker): ThunkType => async (dispatch) => {
		if (!goalList) return;

		const id = await habitTrackerService.create(goalList);
		const newGoalList: IHabitTracker = {
			...goalList,
			id,
		};
		dispatch(actions.addGoalsList(newGoalList));
	},
	deleteGoalsList: (trackerId: number): ThunkType => async (dispatch) => {
		await habitTrackerService.delete(trackerId);
		dispatch(actions.deleteGoalsList(trackerId));
	},
};

export type GoalsListsActions =
	| ActionsUnion<typeof actions>
	| HabitTrackerActions;
type ThunkType = BaseThunkType<GoalsListsActions>;

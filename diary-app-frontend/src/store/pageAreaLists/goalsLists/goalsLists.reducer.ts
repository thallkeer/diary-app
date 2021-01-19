import { IHabitDay, IHabitTracker } from "models";
import { IGoalList } from "models/entities";
import { IDiaryListWrapperCollectionState } from "models/states";
import { ListWrapperUrls } from "models/types";
import {
	createHabitTrackerReducer,
	habitTrackerInitialState,
	habitTrackerReducer,
	IHabitTrackerState,
} from "store/diaryLists/habitTrackers.reducer";
import { ListCollectionHandler } from "../listCollectionHandler";
import { GoalsListsActions } from "./goalsLists.actions";

export interface IGoalsListState {
	goalListId: number;
	goalsAreaId: number;
	listState: IHabitTrackerState;
}

export interface IGoalsListsState
	extends IDiaryListWrapperCollectionState<IGoalsListState> {}

const initialState: IGoalsListsState = {
	byName: {},
};
const GOAL_LIST: ListWrapperUrls = "goalLists";

type HabitTrackerReducerType = typeof habitTrackerReducer;

class GoalsListsCollectionHandler extends ListCollectionHandler<
	IGoalsListsState,
	IGoalsListState,
	IGoalList,
	IHabitTrackerState,
	IHabitTracker,
	IHabitDay,
	HabitTrackerReducerType
> {
	listNamePrefix = GOAL_LIST;

	createListReducer(listName: string) {
		return createHabitTrackerReducer(listName);
	}

	listStateCreator(goalList: IGoalList) {
		const goalsListState: IGoalsListState = {
			goalListId: goalList.id,
			goalsAreaId: goalList.areaOwnerId,
			listState: {
				...habitTrackerInitialState,
				list: goalList.list,
				listName: "habitTracker_" + goalList.list.id,
			},
		};
		return goalsListState;
	}
}

export const goalsListsHandler = new GoalsListsCollectionHandler();

export const goalsListsReducer = (
	state = initialState,
	action: GoalsListsActions
): IGoalsListsState => {
	switch (action.type) {
		case "SET_GOALS_LIST":
			return goalsListsHandler.handleSetLists(action.payload);

		case "ADD_GOALS_LIST":
			return goalsListsHandler.handleAddList(state, action.payload);

		case "DELETE_GOALS_LIST":
			return goalsListsHandler.handleDeleteList(state, action.payload);

		default:
			return goalsListsHandler.handleListAction(
				state,
				action.subjectName,
				(reducer, goalListState) => reducer(goalListState.listState, action)
			);
	}
};

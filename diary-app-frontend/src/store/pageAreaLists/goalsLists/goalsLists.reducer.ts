import { IGoalList } from "models/entities";
import { ListsStateByName } from "models/states";
import {
	createHabitTrackerReducer,
	habitTrackerInitialState,
	HabitTrackerReducerType,
	IHabitTrackerState,
} from "store/diaryLists/habitTrackers.reducer";
import {
	ListCollectionHandler,
	ReducerCollection,
} from "../listCollectionHandler";
import { GoalsListsActions } from "./goalsLists.actions";

export interface IGoalsListState {
	goalListId: number;
	goalsAreaId: number;
	listState: IHabitTrackerState;
}

class GoalsListsReducerCollection extends ReducerCollection<
	HabitTrackerReducerType,
	IGoalList,
	IGoalsListState
> {
	reducerNamePrefix = "goalLists";

	createReducer(listName: string) {
		return createHabitTrackerReducer(listName);
	}

	createState(goalList: IGoalList) {
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

export const goalsListsHandler = new ListCollectionHandler<
	IGoalsListState,
	IGoalList,
	IHabitTrackerState,
	HabitTrackerReducerType
>(new GoalsListsReducerCollection());

export const goalsListsReducer = (
	state = {} as ListsStateByName<IGoalsListState>,
	action: GoalsListsActions
): ListsStateByName<IGoalsListState> => {
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

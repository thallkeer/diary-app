import { ListsStateByName } from "models/states";
import {
	createHabitTrackerReducer,
	habitTrackerInitialState,
	IHabitTrackerState,
} from "store/diaryLists/habitTrackers.reducer";
import {
	ListCollectionHandler,
	ReducerCollection,
} from "../listCollectionHandler";
import { GoalsListsActions } from "./goalsLists.actions";

export const goalsListsHandler = new ListCollectionHandler(
	new ReducerCollection(
		habitTrackerInitialState,
		createHabitTrackerReducer,
		"goalLists"
	)
);

export const goalsListsReducer = (
	state = {} as ListsStateByName<IHabitTrackerState>,
	action: GoalsListsActions
): ListsStateByName<IHabitTrackerState> => {
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
				(reducer, goalListState) => reducer(goalListState, action)
			);
	}
};

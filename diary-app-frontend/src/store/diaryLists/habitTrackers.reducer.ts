import { IHabitDay, IHabitTracker } from "models";
import { IListState } from "models/states";
import { createNamedReducer } from "utils";
import { HabitTrackerActions } from "./habitTrackers.actions";
import { listReducer } from "./lists.reducer";

export interface IHabitTrackerState
	extends IListState<IHabitTracker, IHabitDay> {}

export const habitTrackerInitialState: IHabitTrackerState = {
	list: null,
	listName: "habitTracker",
};

export const habitTrackerReducer = (
	state = habitTrackerInitialState,
	action: HabitTrackerActions
) => listReducer<IHabitTrackerState, IHabitTracker, IHabitDay>(state, action);

export const createHabitTrackerReducer = (reducerName: string) => {
	return createNamedReducer(
		habitTrackerReducer,
		habitTrackerInitialState,
		reducerName
	);
};

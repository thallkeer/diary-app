import { IHabitDay, IHabitTracker } from "models";
import { IListState } from "models/states";
import { ActionsUnion } from "store/actions/action-helpers";
import { DiaryListComponent } from "./lists.reducer";

class HabitTrackerComponent extends DiaryListComponent<
	IHabitTracker,
	IHabitDay
> {}

export const habitTrackerComponent = new HabitTrackerComponent(
	"habitTrackers",
	"habitDays"
);

const habitTrackerActions = habitTrackerComponent.getActions("habitTracker");
export type HabitTrackerActions = ActionsUnion<typeof habitTrackerActions>;

export interface IHabitTrackerState
	extends IListState<IHabitTracker, IHabitDay> {}

export const habitTrackerInitialState: IHabitTrackerState = {
	list: null,
};

export const habitTrackerReducer = habitTrackerComponent.getReducer(
	habitTrackerInitialState,
	"habitTrackers"
);

export type HabitTrackerReducerType = typeof habitTrackerReducer;

export const createHabitTrackerReducer = (reducerName: string) => {
	return habitTrackerComponent.getReducer(
		habitTrackerInitialState,
		reducerName
	);
};

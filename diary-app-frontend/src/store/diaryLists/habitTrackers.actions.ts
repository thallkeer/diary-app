import { IHabitDay, IHabitTracker } from "models";
import { ActionsUnion } from "../actions/action-helpers";
import {
	createListActions,
	getListActions,
	getListItemActions,
} from "./lists.actions";

export const habitTrackerActions = {
	...createListActions<IHabitTracker, IHabitDay>(),
};

export const habitTrackerThunks = {
	...getListActions<IHabitTracker, IHabitDay>("habitTrackers"),
	...getListItemActions<IHabitDay>("habitDays"),
};

export type HabitTrackerActions = ActionsUnion<typeof habitTrackerActions>;

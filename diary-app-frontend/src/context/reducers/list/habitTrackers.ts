import { IHabitDay, IHabitTracker } from "../../../models/entities";
import { IListState } from "../../../models/states";
import { createNamedWrapperReducer } from "../../../utils";
import { ActionsUnion } from "../../actions/action-helpers";
import {
	createListActions,
	getListActions,
} from "../../actions/listCrud-actions";
import { listReducer } from "./listReducer";

export interface IHabitTrackerState
	extends IListState<IHabitTracker, IHabitDay> {}

const initialState: IHabitTrackerState = {
	list: null,
	listName: "habitTracker",
};

export const habitTrackerReducer = (
	state = initialState,
	action: HabitTrackerActions
) => listReducer<IHabitTrackerState, IHabitTracker, IHabitDay>(state, action);

export const createHabitTrackerReducer = (reducerName: string) => {
	return createNamedWrapperReducer(
		habitTrackerReducer,
		initialState,
		reducerName
	);
};

export const habitTrackerActions = {
	...createListActions<IHabitTracker, IHabitDay>(),
};

export const habitTrackerActionCreators = {
	...getListActions<IHabitTracker, IHabitDay>("habitTrackers"),
};

export type HabitTrackerActions = ActionsUnion<typeof habitTrackerActions>;

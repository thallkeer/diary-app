import { IHabitDay, IHabitTracker } from "models";
import { IListState } from "models/states";
import {
	habitDayService,
	habitTrackerService,
} from "services/concreteListService";
import {
	createGenericListThunks,
	createGenericSlice,
	generateListSliceReducers,
} from "./lists.reducer";

export interface IHabitTrackerState
	extends IListState<IHabitTracker, IHabitDay> {}

export const habitTrackerInitialState: IHabitTrackerState = {
	list: null,
};

const baseReducers =
	generateListSliceReducers<IHabitTrackerState, IHabitTracker, IHabitDay>();

type HabitTrackerReducersType = typeof baseReducers;

export const createHabitTrackerSlice = (listName: string) =>
	createGenericSlice<
		IHabitTrackerState,
		IHabitTracker,
		IHabitDay,
		HabitTrackerReducersType
	>({
		name: listName,
		initialState: habitTrackerInitialState,
		reducers: {},
	});

export type HabitTrackerSlice = ReturnType<typeof createHabitTrackerSlice>;

export const createHabitTrackerThunks = (slice: HabitTrackerSlice) => {
	const baseThunks = createGenericListThunks<
		IHabitTrackerState,
		IHabitTracker,
		IHabitDay
	>(slice.name, habitTrackerService, habitDayService);

	return {
		...baseThunks,
	};
};

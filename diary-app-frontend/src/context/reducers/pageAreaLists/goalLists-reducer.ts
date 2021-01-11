import { IHabitTracker } from "../../../models/entities";
import { IGoalsListsState, ListsStateByName } from "../../../models/states";
import { ListUrls, ListWrapperUrls } from "../../../models/types";
import { getGoalListByName } from "../../../selectors/page-selectors";
import { CrudService } from "../../../services/crudService";
import { ActionsUnion, createAction } from "../../actions/action-helpers";
import { BaseThunkType } from "../../store";
import {
	createHabitTrackerReducer,
	habitTrackerActions,
	habitTrackerReducer,
	IHabitTrackerState,
	HabitTrackerActions,
} from "../list/habitTrackers";

const initialState: IGoalsListsState = {
	byName: {},
};
const GOAL_LIST: ListWrapperUrls = "goalLists";
const GOAL_LIST_PATTERN = GOAL_LIST + "_";
export const getGoalListName = (listId: number) => GOAL_LIST_PATTERN + listId;

type HabitTrackerReducerType = typeof habitTrackerReducer;
const reducers: Map<string, HabitTrackerReducerType> = new Map<
	string,
	HabitTrackerReducerType
>();

export const goalsListsReducer = (
	state = initialState,
	action: GoalsListsActions
): IGoalsListsState => {
	switch (action.type) {
		case "SET_TRACKERS":
			reducers.clear();
			const newState: ListsStateByName<IHabitTrackerState> = {};

			action.payload.forEach((dl) => {
				newState[getGoalListName(dl.id)] = createGoalListState(dl);
			});
			return {
				...state,
				byName: newState,
			};

		case "ADD_TRACKER":
			const newList = action.payload;
			const addedState = createGoalListState(newList);
			let listName = getGoalListName(newList.id);
			return {
				...state,
				byName: {
					...state.byName,
					[listName]: addedState,
				},
			};

		case "DELETE_TRACKER":
			let goalListName = getGoalListName(action.payload);
			const stateAfterDelete = { ...state };
			delete stateAfterDelete.byName[goalListName];
			return stateAfterDelete;

		case "UPDATE_TRACKER":
			return state;

		default:
			return oneListReducer(state, action);
	}
};

const oneListReducer = (
	state: IGoalsListsState,
	action: HabitTrackerActions
): IGoalsListsState => {
	let listKey = action.subjectName;
	const reducer = reducers.get(listKey);
	if (!reducer) return state;
	const goalList = getGoalListByName(listKey)(state);
	const newHabitTrackerState = reducer(goalList, action);
	return {
		...state,
		byName: {
			...state.byName,
			[listKey]: newHabitTrackerState,
		},
	};
};

const createGoalListState = (goalList: IHabitTracker) => {
	const listName = getGoalListName(goalList.id);
	const listReducer = createHabitTrackerReducer(listName);
	reducers.set(listName, listReducer);
	const trackerState: IHabitTrackerState = {
		list: {
			...goalList,
			items: goalList.items,
		},
		listName,
	};
	return trackerState;
};

export const SET_TRACKERS = "SET_TRACKERS";
export const ADD_TRACKER = "ADD_TRACKER";
export const UPDATE_TRACKER = "UPDATE_TRACKER";
export const DELETE_TRACKER = "DELETE_TRACKER";

const actions = {
	setTrackers: (trackers: IHabitTracker[]) =>
		createAction(SET_TRACKERS, trackers),
	addTracker: (tracker: IHabitTracker) => createAction(ADD_TRACKER, tracker),
	updateTracker: (tracker: IHabitTracker) =>
		createAction(UPDATE_TRACKER, tracker),
	deleteTracker: (trackerId: number) => createAction(DELETE_TRACKER, trackerId),
	...habitTrackerActions,
};

const trackerUrl: ListUrls = "habitTrackers";
const trackerService = new CrudService<IHabitTracker>(trackerUrl);

export const goalsListsActions = {
	setGoalsLists: (goalsLists: IHabitTracker[]): ThunkType => async (
		dispatch
	) => {
		dispatch(actions.setTrackers(goalsLists));
	},
	addOrUpdateTracker: (tracker: IHabitTracker): ThunkType => async (
		dispatch
	) => {
		if (!tracker) return;

		if (tracker.id === 0) {
			const id = await trackerService.create(tracker);
			tracker.id = id;
			dispatch(actions.addTracker(tracker));
		} else {
			await trackerService.update(tracker);
			dispatch(actions.updateTracker(tracker));
		}
	},
	deleteTracker: (trackerId: number): ThunkType => async (dispatch) => {
		await trackerService.delete(trackerId);
		dispatch(actions.deleteTracker(trackerId));
	},
};

export type GoalsListsActions = ActionsUnion<typeof actions>;
type ThunkType = BaseThunkType<GoalsListsActions>;

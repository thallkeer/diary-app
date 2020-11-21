 import { IPageAreaState, IHabitsTracker } from "../../../models";
// import { PageAreaActions, pageAreaReducer } from "./pageArea-reducer";
// import { getGoalsLists } from "../../../selectors";
// import { BaseThunkType } from "../../store";
// import { ActionsUnion, createAction } from "../../actions/action-helpers";
// import axios from "../../../axios/axios";

// export interface IGoalsAreaState extends IPageAreaState<IGoalsArea> {}

// export const ADD_TRACKER = "ADD_TRACKER";
// export const UPDATE_TRACKER = "UPDATE_TRACKER";
// export const DELETE_TRACKER = "DELETE_TRACKER";

// export const goalsAreaReducer = (
// 	state: IGoalsAreaState,
// 	action: GoalsAreaActions
// ): IGoalsAreaState => {
// 	const area = state.area;

// 	switch (action.type) {
// 		case "ADD_TRACKER": {
// 			return {
// 				...state,
// 				area: {
// 					...area,
// 					goalsLists: [...getGoalsLists(state), action.payload],
// 				},
// 			};
// 		}

// 		case "UPDATE_TRACKER":
// 			return {
// 				...state,
// 				area: {
// 					...area,
// 					goalsLists: getGoalsLists(state).map((t) =>
// 						t.id === action.payload.id ? action.payload : t
// 					),
// 				},
// 			};

// 		case "DELETE_TRACKER":
// 			return {
// 				...state,
// 				area: {
// 					...area,
// 					goalsLists: getGoalsLists(state).filter(
// 						(t) => t.id !== action.payload.id
// 					),
// 				},
// 			};

// 		default:
// 			return pageAreaReducer<IGoalsArea, IGoalsAreaState>(state, action);
// 	}
// };

// const goalsAreaActions = {
// 	addTracker: (tracker: IHabitsTracker) => createAction(ADD_TRACKER, tracker),
// 	updateTracker: (tracker: IHabitsTracker) =>
// 		createAction(UPDATE_TRACKER, tracker),
// 	deleteTracker: (tracker: IHabitsTracker) =>
// 		createAction(DELETE_TRACKER, tracker),
// };

// const baseTrackerApi: string = "habitTracker";

// export const addOrUpdateTracker = (
// 	tracker: IHabitsTracker
// ): ThunkType => async (dispatch) => {
// 	if (!tracker) return;

// 	if (tracker.id === 0) {
// 		await axios.post(baseTrackerApi, tracker).then((res) => {
// 			tracker.id = res.data;
// 			dispatch(goalsAreaActions.addTracker(tracker));
// 		});
// 	} else {
// 		await axios.put(baseTrackerApi, tracker);
// 		dispatch(goalsAreaActions.updateTracker(tracker));
// 	}
// };

// export const deleteTracker = (tracker: IHabitsTracker): ThunkType => async (
// 	dispatch
// ) => {
// 	if (!tracker) return;

// 	await axios.delete(baseTrackerApi + `/${tracker.id}`);
// 	dispatch(goalsAreaActions.deleteTracker(tracker));
// };

// export type GoalsAreaActions = ActionsUnion<typeof goalsAreaActions>;
// type ThunkType = BaseThunkType<GoalsAreaActions>;

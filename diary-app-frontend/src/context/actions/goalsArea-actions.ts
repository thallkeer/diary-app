import { ActionsUnion, createAction } from "./action-helpers";
import { IHabitsTracker, IGoalsArea } from "../../models";
import axios from "../../axios/axios";
import { PageAreaBaseActionsGen } from "./pageArea-actions";

export const ADD_TRACKER = "ADD_TRACKER";
export const UPDATE_TRACKER = "UPDATE_TRACKER";
export const DELETE_TRACKER = "DELETE_TRACKER";

const goalsAreaBaseActions = PageAreaBaseActionsGen<IGoalsArea>();
export const goalsAreaActions = {
	...goalsAreaBaseActions,
	addTracker: (tracker: IHabitsTracker) => createAction(ADD_TRACKER, tracker),
	updateTracker: (tracker: IHabitsTracker) =>
		createAction(UPDATE_TRACKER, tracker),
	deleteTracker: (tracker: IHabitsTracker) =>
		createAction(DELETE_TRACKER, tracker),
};

const baseTrackerApi: string = "habitTracker";

export async function addOrUpdateTracker(
	tracker: IHabitsTracker,
	dispatch: React.Dispatch<GoalsAreaActions>
) {
	if (!tracker) return;

	if (tracker.id === 0) {
		await axios.post(baseTrackerApi, tracker).then((res) => {
			tracker.id = res.data;
			dispatch(goalsAreaActions.addTracker(tracker));
		});
	} else {
		await axios.put(baseTrackerApi, tracker);
		dispatch(goalsAreaActions.updateTracker(tracker));
	}
}

export async function deleteTracker(
	tracker: IHabitsTracker,
	dispatch: React.Dispatch<GoalsAreaActions>
) {
	if (!tracker) return;

	await axios.delete(baseTrackerApi + `/${tracker.id}`);
	dispatch(goalsAreaActions.deleteTracker(tracker));
}

export type GoalsAreaActions = ActionsUnion<typeof goalsAreaActions>;

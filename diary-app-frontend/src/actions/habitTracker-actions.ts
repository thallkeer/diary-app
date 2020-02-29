import { ActionsUnion, createAction } from "./action-helpers";
import { IHabitsTracker } from "../models";
import axios from "../axios/axios";

export const ADD_TRACKER = "ADD_TRACKER";
export const UPDATE_TRACKER = "UPDATE_TRACKER";

const Actions = {
  addTracker: (tracker: IHabitsTracker) => createAction(ADD_TRACKER, tracker),
  updateTracker: (tracker: IHabitsTracker) =>
    createAction(UPDATE_TRACKER, tracker)
};

const baseTrackerApi: string = "habitTracker";

export const Thunks = {
  addOrUpdateTracker: (tracker: IHabitsTracker) => {
    return dispatch => {
      if (!tracker) return;

      console.log("before add or update ", tracker);

      if (tracker.id === 0) {
        console.log("add tracker");

        axios.post(baseTrackerApi, tracker).then(res => {
          tracker.id = res.data;
          dispatch(Actions.addTracker(tracker));
        });
      } else {
        console.log("update tracker");

        axios.put(baseTrackerApi, tracker).then(res => {
          dispatch(Actions.updateTracker(tracker));
        });
      }
    };
  }
};

export type HabitTrackerActions = ActionsUnion<typeof Actions>;
export type HabitTrackerThunks = ActionsUnion<typeof Thunks>;

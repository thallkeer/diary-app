import { ActionsUnion, createAction } from "./action-helpers";
import { IHabitsTracker } from "../../models";
import axios from "../../axios/axios";

export const ADD_TRACKER = "ADD_TRACKER";
export const UPDATE_TRACKER = "UPDATE_TRACKER";
export const DELETE_TRACKER = "DELETE_TRACKER";

const Actions = {
  addTracker: (tracker: IHabitsTracker) => createAction(ADD_TRACKER, tracker),
  updateTracker: (tracker: IHabitsTracker) =>
    createAction(UPDATE_TRACKER, tracker),
  deleteTracker: (tracker: IHabitsTracker) =>
    createAction(DELETE_TRACKER, tracker),
};

const baseTrackerApi: string = "habitTracker";

export const Thunks = {
  addOrUpdateTracker: (tracker: IHabitsTracker) => {
    return (dispatch) => {
      if (!tracker) return;

      if (tracker.id === 0) {
        axios.post(baseTrackerApi, tracker).then((res) => {
          tracker.id = res.data;
          dispatch(Actions.addTracker(tracker));
        });
      } else {
        axios.put(baseTrackerApi, tracker).then((res) => {
          dispatch(Actions.updateTracker(tracker));
        });
      }
    };
  },

  deleteTracker: (tracker: IHabitsTracker) => {
    return (dispatch) => {
      if (!tracker) return;
      console.log(tracker);

      axios
        .delete(baseTrackerApi + `/${tracker.id}`)
        .then((res) => dispatch(Actions.deleteTracker(tracker)));
    };
  },
};

export type HabitTrackerActions = ActionsUnion<typeof Actions>;
export type HabitTrackerThunks = ActionsUnion<typeof Thunks>;

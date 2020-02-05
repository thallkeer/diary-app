import { HabitTrackerActions } from "../actions/habitTracker-actions";
import { IHabitTrackerContext } from ".";

export const trackersReducer = (
  state: IHabitTrackerContext,
  action: HabitTrackerActions
): IHabitTrackerContext => {
  switch (action.type) {
    case "ADD_TRACKER": {
      return {
        ...state,
        trackers: [...state.trackers, action.payload]
      };
    }

    case "UPDATE_TRACKER":
      return {
        ...state,
        trackers: state.trackers.map(t =>
          t.id === action.payload.id ? action.payload : t
        )
      };

    default:
      return state;
  }
};

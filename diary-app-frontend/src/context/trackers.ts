import { HabitTrackerActions } from "./actions/habitTracker-actions";
import { IGoalsArea } from "../models";

export const trackersReducer = (
  state: IGoalsArea,
  action: HabitTrackerActions
): IGoalsArea => {
  switch (action.type) {
    case "ADD_TRACKER": {
      console.log("add payload", action.payload);
      return {
        ...state,
        goalsLists: [...state.goalsLists, action.payload]
      };
    }

    case "UPDATE_TRACKER":
      console.log("update payload", action.payload);

      return {
        ...state,
        goalsLists: state.goalsLists.map(t =>
          t.id === action.payload.id ? action.payload : t
        )
      };

    default:
      return state;
  }
};

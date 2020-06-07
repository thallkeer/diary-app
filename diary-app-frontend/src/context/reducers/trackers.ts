import { HabitTrackerActions } from "../actions/habitTracker-actions";
import { IGoalsArea } from "../../models";

export const trackersReducer = (
  state: IGoalsArea,
  action: HabitTrackerActions
): IGoalsArea => {
  switch (action.type) {
    case "ADD_TRACKER": {
      return {
        ...state,
        goalsLists: [...state.goalsLists, action.payload],
      };
    }

    case "UPDATE_TRACKER":
      return {
        ...state,
        goalsLists: state.goalsLists.map((t) =>
          t.id === action.payload.id ? action.payload : t
        ),
      };

    case "DELETE_TRACKER":
      return {
        ...state,
        goalsLists: state.goalsLists.filter((t) => t.id !== action.payload.id),
      };

    default:
      return state;
  }
};

import { ActionTypes } from "./action-types";
import { ILightEvent } from "../models";
import { EventsActions } from "../actions/events-actions";
import { IAppContext } from "./app-state";

export const eventsReducer = (
  state: IAppContext,
  action: EventsActions
): IAppContext => {
  switch (action.type) {
    case ActionTypes.LOAD_EVENTS + ActionTypes.START:
      console.log("reducer", action);

      return { ...state, loading: true };
    case ActionTypes.LOAD_EVENTS + ActionTypes.SUCCESS:
      // if (events) {
      //   events.forEach(event => {
      //     event.date = moment(event.date).toDate();
      //   });
      // }
      console.log("reducer", action);

      return {
        ...state,
        events: action.payload as ILightEvent[],
        loading: false
      };
    case ActionTypes.ADD_EVENT:
      return {
        ...state,
        events: [...state.events, action.payload as ILightEvent]
      };
    default:
      return state;
  }
};

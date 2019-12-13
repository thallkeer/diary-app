import { ActionTypes } from "./action-types";
import { ILightEvent } from "../models";
import { EventsActions } from "../actions/events-actions";
import moment from "moment";
import { ApplicationContext } from ".";

export const eventsReducer = (
  state: ApplicationContext,
  action: EventsActions
): ApplicationContext => {
  switch (action.type) {
    case ActionTypes.LOAD_EVENTS + ActionTypes.START:
      return { ...state, loading: true };

    case ActionTypes.LOAD_EVENTS + ActionTypes.SUCCESS:
      const events = action.payload as ILightEvent[];
      if (events) {
        events.forEach(event => {
          event.date = moment(event.date).toDate();
        });
      }

      return {
        ...state,
        events,
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

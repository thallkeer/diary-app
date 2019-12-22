import { ActionTypes } from "./action-types";
import { ILightEvent, IEventList } from "../models";
import { EventsActions } from "../actions/events-actions";
import { ApplicationContext } from ".";
import { getEvents } from "../selectors/events";

export const eventsReducer = (
  state: ApplicationContext,
  action: EventsActions
): ApplicationContext => {
  switch (action.type) {
    case ActionTypes.LOAD_EVENTS + ActionTypes.START:
      return { ...state, loading: true };

    case ActionTypes.LOAD_EVENTS + ActionTypes.SUCCESS: {
      console.log(action.payload);

      const eventList = action.payload as IEventList;
      const events = eventList.items;
      if (events) events.forEach(event => (event.date = new Date(event.date)));

      let newEventList: IEventList = { ...eventList, items: [...events] };

      console.log(newEventList);

      return {
        ...state,
        eventList: newEventList,
        loading: false
      };
    }
    case ActionTypes.ADD_EVENT:
      let newEventList: IEventList = {
        ...state.eventList,
        items: [...state.eventList.items, action.payload as ILightEvent]
      };
      return {
        ...state,
        eventList: newEventList
      };

    case ActionTypes.DELETE_EVENT:
      return {
        ...state,
        eventList: {
          ...state.eventList,
          items: getEvents(state).filter(
            event => event.eventID !== action.payload
          )
        }
      };
    default:
      return state;
  }
};

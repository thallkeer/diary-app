import { ActionTypes } from "./action-types";
import { IEvent, IEventList } from "../models";
import { EventsActions } from "../actions/events-actions";
import { ListState } from ".";
import { getEvents } from "../selectors";

export const eventsReducer = (
  state: ListState<IEvent>,
  action: EventsActions
): ListState<IEvent> => {
  switch (action.type) {
    case ActionTypes.LOAD_EVENTS + ActionTypes.START:
      return { ...state, loading: true };

    case ActionTypes.LOAD_EVENTS + ActionTypes.SUCCESS: {
      const eventList = action.payload as IEventList;
      const events = eventList.items ? [...eventList.items] : [];

      events.forEach(event => (event.date = new Date(event.date)));

      let newEventList = {
        ...eventList,
        items: events
      };

      return {
        ...state,
        list: newEventList,
        loading: false
      };
    }
    case ActionTypes.ADD_EVENT:
      let addedEvent = action.payload as IEvent;
      addedEvent.date = new Date(addedEvent.date);

      let newEventList = {
        ...state.list,
        items: [...state.list.items, addedEvent]
      };
      return {
        ...state,
        list: newEventList
      };

    case ActionTypes.DELETE_EVENT:
      return {
        ...state,
        list: {
          ...state.list,
          items: getEvents(state).filter(event => event.id !== action.payload)
        }
      };
    default:
      return state;
  }
};

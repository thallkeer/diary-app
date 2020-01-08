import * as eventActions from "../actions/events-actions";
import { IEventListContext } from ".";
import { getEvents } from "../selectors";

export const eventsReducer = (
  state: IEventListContext,
  action: eventActions.EventActions
): IEventListContext => {
  switch (action.type) {
    case "LOAD_EVENTS_START":
      return { ...state, loading: true };

    case "LOAD_EVENTS": {
      const eventList = action.payload;
      const events = eventList.items || [];

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
    case "ADD_EVENT":
      let addedEvent = action.payload;
      addedEvent.date = new Date(addedEvent.date);

      let newEventList = {
        ...state.list,
        items: [...state.list.items, addedEvent]
      };
      return {
        ...state,
        list: newEventList
      };

    case "DELETE_EVENT":
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

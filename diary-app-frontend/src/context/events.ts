import { EventActions } from "./actions/events-actions";
import { IEventListContext } from ".";
import { getEvents } from "../selectors";

export const eventsReducer = (
  state: IEventListContext,
  action: EventActions
): IEventListContext => {
  switch (action.type) {
    case "LOAD_EVENTS_START":
      return { ...state, loading: true };

    case "LOAD_EVENTS": {
      const eventList = action.payload;
      console.log("events loaded", eventList);

      const events = eventList.items || [];

      return {
        ...state,
        list: {
          ...eventList,
          items: events.map(event => {
            return { ...event, date: new Date(event.date) };
          })
        },
        loading: false
      };
    }
    case "ADD_EVENT":
      let addedEvent = action.payload;
      addedEvent.date = new Date(addedEvent.date);

      return {
        ...state,
        list: {
          ...state.list,
          items: [...getEvents(state), addedEvent]
        }
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
      console.log("reducer", state);
      return state;
  }
};

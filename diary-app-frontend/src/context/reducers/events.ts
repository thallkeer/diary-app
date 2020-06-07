import { EventActions } from "../actions/events-actions";
import { IEventListContext } from "..";
import { getEvents } from "../../selectors";

export const eventsReducer = (
  state: IEventListContext,
  action: EventActions
): IEventListContext => {
  switch (action.type) {
    case "LOAD_EVENTS_START":
      return { ...state, loading: true };

    case "LOAD_EVENTS": {
      const eventList = action.payload;
      const events = eventList.items || [];

      return {
        ...state,
        list: {
          ...eventList,
          items: events.map((event) => {
            return { ...event, date: new Date(event.date) };
          }),
        },
        loading: false,
      };
    }
    case "ADD_EVENT":
      let addedEvent = action.payload;

      addedEvent.date = new Date(addedEvent.date);

      return {
        ...state,
        list: {
          ...state.list,
          items: [...getEvents(state), addedEvent],
        },
      };

    case "UPDATE_EVENT":
      return {
        ...state,
        list: {
          ...state.list,
          items: getEvents(state).map((item) =>
            item.id === action.payload.id ? action.payload : item
          ),
        },
      };

    case "DELETE_EVENT":
      return {
        ...state,
        list: {
          ...state.list,
          items: getEvents(state).filter(
            (event) => event.id !== action.payload
          ),
        },
      };

    case "UPDATE_EVENTLIST":
      return {
        ...state,
        list: action.payload,
      };

    case "DELETE_EVENT_LIST":
      return {
        ...state,
        list: null,
      };

    default:
      console.log("unknown action type reducer", action, state);
      return state;
  }
};
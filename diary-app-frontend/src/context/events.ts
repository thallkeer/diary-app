import { ActionTypes } from "./action-types";
import { ILightEvent, IList } from "../models";
import { EventsActions } from "../actions/events-actions";
import { BaseState } from ".";
import { getEvents } from "../selectors";

export const eventsReducer = (
  state: BaseState<ILightEvent>,
  action: EventsActions
): BaseState<ILightEvent> => {
  switch (action.type) {
    case ActionTypes.LOAD_EVENTS + ActionTypes.START:
      return { ...state, loading: true };

    case ActionTypes.LOAD_EVENTS + ActionTypes.SUCCESS: {
      const eventList = action.payload as IList<ILightEvent>;
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
      let addedEvent = action.payload as ILightEvent;
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

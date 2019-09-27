import ILightEvent from "../models/event-light-model";
import { ActionTypes } from "../actions/action-types";
import * as eventActions from "../actions/events-actions";
import moment from "moment";

export interface IEventReducerState {
  events: ILightEvent[];
  loading: boolean;
  loaded: boolean;
}

export const initialState: IEventReducerState = {
  events: [],
  loading: false,
  loaded: false
};

export function reducer(
  state: IEventReducerState = initialState,
  action: eventActions.Actions
): IEventReducerState {
  switch (action.type) {
    case ActionTypes.ADD_EVENT: {
      const payload = action.payload;
      console.log("add event payload: ", payload);

      return {
        ...state,
        events: [...state.events]
      };
    }

    case ActionTypes.LOAD_EVENTS + ActionTypes.START:
      return {
        ...state,
        loading: true,
        loaded: false
      };

    case ActionTypes.LOAD_EVENTS + ActionTypes.SUCCESS: {
      var events: ILightEvent[] = action.payload as ILightEvent[];
      if (events) {
        events.forEach(event => {
          event.date = moment(event.date).toDate();
        });
      }

      return {
        ...state,
        loading: false,
        loaded: true,
        events: action.payload as ILightEvent[]
      };
    }

    // case ActionTypes.UPDATE_TODO: {
    //   const { todoId, text } = action.payload;
    //   const newTodos = state.todos;
    //   const index = newTodos.findIndex(el => el.id === todoId);
    //   const addNewItem = newTodos[index].text === "";

    //   newTodos[index].text = text;

    //   if (addNewItem)
    //     newTodos.push({ id: getRandomId(), text: "", done: false });

    //   return {
    //     ...state,
    //     todos: newTodos
    //   };
    // }

    default:
      return state;
  }
}

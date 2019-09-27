import { ActionsUnion, createAction } from "./action-helpers";
import { Dispatch } from "redux";
import { ActionTypes } from "./action-types";
import ILightEvent from "../models/event-light-model";

export const EventActions = {
  startLoadEvents: () =>
    createAction(ActionTypes.LOAD_EVENTS + ActionTypes.START),
  finishLoadEvents: (events: ILightEvent[]) =>
    createAction(ActionTypes.LOAD_EVENTS + ActionTypes.SUCCESS, events),
  updateEvent: (eventId: number, title: string) =>
    createAction(ActionTypes.UPDATE_EVENT, {
      eventId: eventId,
      eventTitle: title
    }),
  addEvent: (title: string, date: Date) =>
    createAction(ActionTypes.ADD_EVENT, { eventTitle: title, eventDate: date })
};

//поставить правильный апи
const callApi: string = `https://localhost:44320/api/events/${new Date().getMonth() +
  1}`;

export const Thunks = {
  loadEvents: () => {
    return (dispatch: Dispatch) => {
      dispatch(EventActions.startLoadEvents());
      fetch(callApi)
        .then(res => res.json())
        .then(response => dispatch(EventActions.finishLoadEvents(response)));
    };
  },

  updateEvent: (eventId: number, title: string) => {
    return (dispatch: Dispatch) => {
      dispatch(EventActions.updateEvent(eventId, title));
    };
  },

  addEvent: (title: string, date: Date) => {
    return (dispatch: Dispatch) => {
      dispatch(EventActions.addEvent(title, date));
    };
  }
};

export type Actions = ActionsUnion<typeof EventActions>;

export type Thunks = ActionsUnion<typeof Thunks>;

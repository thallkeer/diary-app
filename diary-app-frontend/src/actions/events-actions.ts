import { ActionsUnion, createAction } from "./action-helpers";
import { ActionTypes } from "../contexts/action-types";
import { ILightEvent, IEventList } from "../models/index";
import axios from "axios";

export const EventActions = {
  startLoadEvents: () =>
    createAction(ActionTypes.LOAD_EVENTS + ActionTypes.START),
  finishLoadEvents: (events: IEventList) =>
    createAction(ActionTypes.LOAD_EVENTS + ActionTypes.SUCCESS, events),
  updateEvent: (eventId: number, title: string) =>
    createAction(ActionTypes.UPDATE_EVENT, {
      eventId: eventId,
      eventTitle: title
    }),
  addEvent: (title: string, date: Date) =>
    createAction(ActionTypes.ADD_EVENT, { eventTitle: title, eventDate: date }),
  deleteEvent: (eventID: number) =>
    createAction(ActionTypes.DELETE_EVENT, eventID)
};

const baseApi = "https://localhost:44320/api/events/";

export const Thunks = {
  loadEvents: (title: string) => {
    return dispatch => {
      dispatch(EventActions.startLoadEvents());
      axios
        .get(baseApi + `${new Date().getMonth() + 1}/title/${title}`)
        .then(response =>
          dispatch(EventActions.finishLoadEvents(response.data))
        );
    };
  },

  updateEvent: (eventId: number, title: string) => {
    return dispatch => {
      dispatch(EventActions.updateEvent(eventId, title));
    };
  },

  addEvent: (title: string, date: Date) => {
    return dispatch => {
      dispatch(EventActions.addEvent(title, date));
    };
  },

  deleteEvent: (eventID: number) => {
    return dispatch => {
      axios
        .delete(baseApi + `${eventID}`)
        .then(dispatch(EventActions.deleteEvent(eventID)));
    };
  }
};

export type EventsActions = ActionsUnion<typeof EventActions>;
export type EventThunks = typeof Thunks;

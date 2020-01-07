import { ActionsUnion, createAction } from "./action-helpers";
import { IEvent, IEventList } from "../models/index";
import axios from "axios";

export const ADD_EVENT = "ADD_EVENT";
export const UPDATE_EVENT = "UPDATE_EVENT";
export const LOAD_EVENTS_START = "LOAD_EVENTS_START";
export const LOAD_EVENTS = "LOAD_EVENTS";
export const DELETE_EVENT = "DELETE_EVENT";

const Actions = {
  startLoadEvents: () => createAction(LOAD_EVENTS_START),
  finishLoadEvents: (events: IEventList) => createAction(LOAD_EVENTS, events),
  updateEvent: (eventId: number, title: string) =>
    createAction(UPDATE_EVENT, {
      eventId: eventId,
      eventTitle: title
    }),
  addEvent: (newEvent: IEvent) => createAction(ADD_EVENT, newEvent),
  deleteEvent: (eventID: number) => createAction(DELETE_EVENT, eventID)
};

const baseApi = "https://localhost:44320/api/events/";

export const Thunks = {
  loadEvents: (pageID: number) => {
    return dispatch => {
      dispatch(Actions.startLoadEvents());
      axios.get(baseApi + pageID).then(response => {
        dispatch(Actions.finishLoadEvents(response.data));
      });
    };
  },

  updateEvent: (eventId: number, title: string) => {
    return dispatch => {
      dispatch(Actions.updateEvent(eventId, title));
    };
  },

  addEvent: (newEvent: IEvent) => {
    return dispatch => {
      axios
        .post(baseApi, newEvent)
        .then(res => dispatch(Actions.addEvent(res.data)));
    };
  },

  deleteEvent: (eventID: number) => {
    return dispatch => {
      axios
        .delete(baseApi + `${eventID}`)
        .then(dispatch(Actions.deleteEvent(eventID)));
    };
  }
};

export type EventActions = ActionsUnion<typeof Actions>;
export type EventThunks = ActionsUnion<typeof Thunks>;

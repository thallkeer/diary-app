import { ActionsUnion, createAction } from "./action-helpers";
import { ActionTypes } from "../context/action-types";
import { IEvent, IEventList } from "../models/index";
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
  addEvent: (newEvent: IEvent) => createAction(ActionTypes.ADD_EVENT, newEvent),
  deleteEvent: (eventID: number) =>
    createAction(ActionTypes.DELETE_EVENT, eventID)
};

const baseApi = "https://localhost:44320/api/events/";

export const Thunks = {
  loadEvents: (pageID: number) => {
    return dispatch => {
      dispatch(EventActions.startLoadEvents());
      axios.get(baseApi + pageID).then(response => {
        dispatch(EventActions.finishLoadEvents(response.data));
      });
    };
  },

  updateEvent: (eventId: number, title: string) => {
    return dispatch => {
      dispatch(EventActions.updateEvent(eventId, title));
    };
  },

  addEvent: (newEvent: IEvent) => {
    return dispatch => {
      axios
        .post(baseApi, newEvent)
        .then(res => dispatch(EventActions.addEvent(res.data)));
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
export type EventThunks = ActionsUnion<typeof Thunks>;

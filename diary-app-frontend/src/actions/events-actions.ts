import { ActionsUnion, createAction } from "./action-helpers";
import { IEvent, IEventList } from "../models/index";
import axios from "../axios/axios";

export const ADD_EVENT = "ADD_EVENT";
export const UPDATE_EVENT = "UPDATE_EVENT";
export const LOAD_EVENTS_START = "LOAD_EVENTS_START";
export const LOAD_EVENTS = "LOAD_EVENTS";
export const DELETE_EVENT = "DELETE_EVENT";
export const UPDATE_EVENTLIST = "UPDATE_EVENTLIST";

const Actions = {
  startLoadEvents: () => createAction(LOAD_EVENTS_START),
  finishLoadEvents: (events: IEventList) => createAction(LOAD_EVENTS, events),
  updateEvent: (event: IEvent) => createAction(UPDATE_EVENT, event),
  addEvent: (newEvent: IEvent) => createAction(ADD_EVENT, newEvent),
  deleteEvent: (eventID: number) => createAction(DELETE_EVENT, eventID),
  updateEventList: (eventList: IEventList) =>
    createAction(UPDATE_EVENTLIST, eventList)
};

const baseEventsApi = `events/`;

export const Thunks = {
  loadEventsByPageID: (pageID: number) => {
    return dispatch => {
      dispatch(Actions.startLoadEvents());
      axios.get(baseEventsApi + pageID).then(response => {
        dispatch(Actions.finishLoadEvents(response.data));
      });
    };
  },

  updateEventList: (eventList: IEventList) => {
    return dispatch => {
      axios.put(baseEventsApi, eventList);
      dispatch(Actions.updateEventList(eventList));
    };
  },

  addOrUpdateEvent: (event: IEvent) => {
    if (!event) return;

    if (event.id === 0) {
      return dispatch => {
        axios
          .post(baseEventsApi + "addEvent", event)
          .then(res => dispatch(Actions.addEvent({ ...event, id: res.data })));
      };
    } else {
      return dispatch => {
        axios
          .put(baseEventsApi, event)
          .then(res => dispatch(Actions.updateEvent(event)));
      };
    }
  },

  deleteEvent: (eventID: number) => {
    return dispatch => {
      axios
        .delete(baseEventsApi + `${eventID}`)
        .then(dispatch(Actions.deleteEvent(eventID)));
    };
  }
};

export type EventActions = ActionsUnion<typeof Actions>;
export type EventThunks = ActionsUnion<typeof Thunks>;

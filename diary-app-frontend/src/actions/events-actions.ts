import { ActionsUnion, createAction } from "./action-helpers";
import { ActionTypes } from "../contexts/action-types";
import { ILightEvent } from "../models/index";

export const EventActions = {
  startLoadEvents: () =>
    createAction(ActionTypes.LOAD_EVENTS + ActionTypes.START),
  finishLoadEvents: (events: ILightEvent[]) =>
    createAction<ILightEvent[]>(
      ActionTypes.LOAD_EVENTS + ActionTypes.SUCCESS,
      events
    ),
  updateEvent: (eventId: number, title: string) =>
    createAction(ActionTypes.UPDATE_EVENT, {
      eventId: eventId,
      eventTitle: title
    }),
  addEvent: (title: string, date: Date) =>
    createAction(ActionTypes.ADD_EVENT, { eventTitle: title, eventDate: date })
};

//поставить правильный апи
const callApi = `https://localhost:44320/api/events/${new Date().getMonth() +
  1}`;

export const Thunks: EventThunks = {
  loadEvents: () => {
    return dispatch => {
      console.log("in load events");
      dispatch(EventActions.startLoadEvents());
      fetch(callApi)
        .then(res => {
          console.log(res);
          return res.json();
        })
        .then(response => dispatch(EventActions.finishLoadEvents(response)));
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
  }
};

export default interface EventThunks {
  loadEvents: (dispatch) => void;
  updateEvent: (eventId: number, title: string) => (dispatch) => void;
  addEvent: (title: string, date: Date) => (dispatch) => void;
}

export type EventsActions = ActionsUnion<typeof EventActions>;

//export type EventThunks = ActionsUnion<typeof Thunks>;

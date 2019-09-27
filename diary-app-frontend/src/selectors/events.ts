import { IAppState } from "../reducers";
import { createSelector } from "reselect";
import IEventsByDay from "../models/events-byday";
const getEventsState = (state: IAppState) => state.events;

export const getEvents = createSelector(
  [getEventsState],
  s => s.events
);

export const getEventsLoading = createSelector(
  [getEventsState],
  s => s.loading
);

export const getEventsLoaded = createSelector(
  [getEventsState],
  s => s.loaded
);

export const getEventsByDay = createSelector(
  [getEventsState],
  s =>
    s.events.map(ev => {
      return { day: ev.date.getDate(), event: ev } as IEventsByDay;
    })
);

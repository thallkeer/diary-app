import { createSelector } from "reselect";
import { IEventsByDay } from "../models/index";
import { IAppContext } from "../contexts/app-state";
const getEventsState = (state: IAppContext) => state;

export const getEvents = createSelector([getEventsState], s => s.events);

export const getEventsByDay = createSelector([getEventsState], s =>
  s.events.map(ev => {
    return { day: ev.date.getDate(), event: ev } as IEventsByDay;
  })
);

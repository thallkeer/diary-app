import { createSelector } from "reselect";
import { IEventsByDay, IEvent } from "../models/index";
import { IEventListContext, ITodoListContext } from "../context";

const getEventsState = (state: IEventListContext) =>
  state && state.list ? state.list.items : [];

const getTodosState = (state: ITodoListContext) =>
  state && state.list ? state.list.items : [];

export const getEvents = createSelector([getEventsState], s => s);

export const getEventsByDay = createSelector([getEventsState], s =>
  s.map((ev: IEvent) => {
    return { day: ev.date.getDate(), event: ev } as IEventsByDay;
  })
);

export const getTodos = createSelector([getTodosState], s => s);

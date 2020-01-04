import { createSelector } from "reselect";
import { IEventsByDay, ITodoItem, IEvent } from "../models/index";
import { BaseState } from "../context";

const getEventsState = (events: BaseState<IEvent>) => events.list.items;

export const getEvents = createSelector([getEventsState], s => s);

export const getEventsByDay = createSelector([getEventsState], s =>
  s.map(ev => {
    return { day: ev.date.getDate(), event: ev } as IEventsByDay;
  })
);

const getTodosState = (state: BaseState<ITodoItem>) => state;

export const getTodos = createSelector([getTodosState], s => s.list.items);

import { createSelector } from "reselect";
import { IEventsByDay, IEvent, ITodo } from "../models/index";
import { ListState, EventListStore } from "../context";

const getEventsState = (state: ListState<IEvent>) => {
  if (state && state.list) return state.list.items;
  return [];
};

export const getEvents = createSelector([getEventsState], s => s);

export const getEventsByDay = createSelector([getEventsState], s =>
  s.map(ev => {
    return { day: ev.date.getDate(), event: ev } as IEventsByDay;
  })
);

const getTodosState = (state: ListState<ITodo>) => state.list.items;

export const getTodos = createSelector([getTodosState], s => s);

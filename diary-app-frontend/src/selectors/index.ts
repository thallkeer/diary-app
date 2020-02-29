import { createSelector } from "reselect";
import { IEvent } from "../models/index";
import { IEventListContext, ITodoListContext } from "../context";

const getEventsState = (state: IEventListContext) =>
  state && state.list ? state.list.items : [];

const getTodosState = (state: ITodoListContext) =>
  state && state.list ? state.list.items : [];

export const getEvents = createSelector([getEventsState], s => s);

export const getEventsByDay = createSelector([getEventsState], s => {
  let eventsMap = new Map<number, IEvent[]>();

  if (s && s.length) {
    s.forEach(ev => {
      let day = ev.date.getDate();
      if (!eventsMap.has(day)) eventsMap.set(day, []);
      eventsMap.get(day).push(ev);
    });
  }

  return eventsMap;
});

export const getTodos = createSelector([getTodosState], s => s);

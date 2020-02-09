import { useReducer, useEffect, useState } from "react";
import { todosReducer } from "../context/todos";
import { TodoThunks, Thunks as todoThunks } from "../actions/todo-actions";
import {
  IPage,
  IEventList,
  ITodoList,
  IList,
  ListItem,
  ITodo,
  IEvent
} from "../models";
import { ITodoListContext, IEventListContext } from "../context";
import { EventThunks, Thunks as eventThunks } from "../actions/events-actions";
import { eventsReducer } from "../context/events";
import { useFillToNumber } from "./useFillToNumber";
import { getEmptyEvent, getEmptyTodo } from "../utils";

export const useEventList = (eventList: IEventList, fillTo: number) =>
  useList<IEventList, IEvent>(eventList, fillTo, getEmptyEvent);

export const useTodoList = (todoList: ITodoList, fillTo: number) =>
  useList<ITodoList, ITodo>(todoList, fillTo, getEmptyTodo);

function useList<T extends IList<TItem>, TItem extends ListItem>(
  list: T,
  fillTo: number,
  getEmptyItem: () => TItem
): [string, React.Dispatch<React.SetStateAction<string>>, TItem[]] {
  const [listTitle, setListTitle] = useState(list.title);
  const items = useFillToNumber([...list.items], fillTo, getEmptyItem);

  return [listTitle, setListTitle, items];
}

export function useTodos(page: IPage, initList?: ITodoList): ITodoListContext {
  const [state, _dispatch] = useReducer(todosReducer, {
    list: initList || null,
    loading: false,
    dispatch: (action: TodoThunks) => action(_dispatch)
  });

  const { dispatch } = state;

  useEffect(() => {
    if (page && !initList) {
      dispatch(todoThunks.loadTodosByPageID(page.id));
    }
  }, [page, initList, dispatch]);

  return state;
}

export function useEvents(
  page: IPage,
  initList?: IEventList
): IEventListContext {
  const [state, _dispatch] = useReducer(eventsReducer, {
    loading: false,
    dispatch: (action: EventThunks) => action(_dispatch),
    list: initList || null
  });

  const { dispatch } = state;

  useEffect(() => {
    if (page && !initList) {
      dispatch(eventThunks.loadEventsByPageID(page.id));
    }
  }, [page, initList, dispatch]);

  return state;
}

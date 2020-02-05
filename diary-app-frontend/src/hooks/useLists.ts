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

export const useEventList = (eventList: IEventList, fillTo: number) => {
  const { title, setTitle, items } = useList<IEventList, IEvent>(
    eventList,
    fillTo,
    getEmptyEvent
  );

  return {
    title,
    setTitle,
    events: items
  };
};

export const useTodoList = (todoList: ITodoList, fillTo: number) => {
  const { title, setTitle, items } = useList<ITodoList, ITodo>(
    todoList,
    fillTo,
    getEmptyTodo
  );

  return {
    title,
    setTitle,
    todos: items
  };
};

function useList<T extends IList<TItem>, TItem extends ListItem>(
  list: T,
  fillTo: number,
  getEmptyItem: () => TItem
) {
  const [listTitle, setListTitle] = useState(list.title);
  const items = useFillToNumber([...list.items], fillTo, getEmptyItem);

  return { title: listTitle, setTitle: setListTitle, items };
}

export function useTodos(page: IPage, initList?: ITodoList): ITodoListContext {
  const [state, _dispatch] = useReducer(todosReducer, {
    list: initList || null,
    loading: false,
    dispatch: (action: TodoThunks) => action(_dispatch)
  });

  useEffect(() => {
    page && !initList && state.dispatch(todoThunks.loadTodos(page.id));
  }, [page, initList]);

  return state;
}

export function useEvents(
  page: IPage,
  initList?: IEventList
): IEventListContext {
  let evList: IEventList = {
    id: 324324324,
    items: [],
    pageId: 5656,
    title: "kfhdsjkhfkjdshfkjds"
  };

  const [state, _dispatch] = useReducer(eventsReducer, {
    loading: false,
    dispatch: (action: EventThunks) => action(_dispatch),
    list: initList
  });

  useEffect(() => {
    if (page && !initList) {
      console.log("effect");
      state.dispatch(eventThunks.loadEventsByPageID(page.id));
    }
  }, [page, initList]);

  console.log("state-", state);

  return state;
}

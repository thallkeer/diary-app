import { useReducer, useEffect } from "react";
import { todosReducer } from "../context/todos";
import { TodoThunks, Thunks as todoThunks } from "../actions/todo-actions";
import { IPage } from "../models";
import { ITodoListContext, IEventListContext } from "../context";
import { EventThunks, Thunks as eventThunks } from "../actions/events-actions";
import { eventsReducer } from "../context/events";

export function useTodos(
  page: IPage
): [ITodoListContext, (action: TodoThunks) => void] {
  const [state, _dispatch] = useReducer(todosReducer, {
    list: null,
    loading: false,
    dispatch: () => {}
  });
  const dispatch = (action: TodoThunks) => action(_dispatch);

  useEffect(() => {
    console.log("useTodos effect");
    page && dispatch(todoThunks.loadTodos(page.id));
  }, [page]);

  return [state, dispatch];
}

export function useEvents(
  page: IPage
): [IEventListContext, (action: EventThunks) => void] {
  const [state, _dispatch] = useReducer(eventsReducer, {
    list: null,
    loading: false,
    dispatch: () => {}
  });
  const dispatch = (action: EventThunks) => action(_dispatch);

  useEffect(() => {
    page && dispatch(eventThunks.loadEvents(page.id));
  }, [page]);

  return [state, dispatch];
}

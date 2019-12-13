import { createContext } from "react";
import { ITodoItem, ILightEvent } from "../models/index";
import { TodoThunks, Thunks as todoThunks } from "../actions/todo-actions";
import { EventThunks, Thunks as eventThunks } from "../actions/events-actions";

export type TodoState = {
  loading: boolean;
  todos: ITodoItem[];
  thunks: TodoThunks;
  dispatch?: (action) => void;
};

export const TodoContext = createContext<TodoState>({
  loading: false,
  todos: [],
  thunks: todoThunks
});

export type ApplicationContext = {
  loading: boolean;
  events: ILightEvent[];
  dispatch?: (action) => void;
  thunks: EventThunks;
};

export const AppContext = createContext<ApplicationContext>({
  events: [],
  loading: false,
  thunks: eventThunks
});

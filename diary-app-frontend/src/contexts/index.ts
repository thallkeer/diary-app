import { createContext } from "react";
import { ITodoItem, IEventList, ILightEvent } from "../models/index";
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
  eventList: IEventList;
  dispatch?: (action) => void;
  thunks: EventThunks;
};

export const appInitialState: ApplicationContext = {
  eventList: { id: 0, items: [], month: 0, pageId: 0, title: "" },
  loading: false,
  thunks: eventThunks
};

export const AppContext = createContext<ApplicationContext>(appInitialState);

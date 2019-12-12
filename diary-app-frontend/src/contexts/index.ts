import { createContext } from "react";
import { ITodoItem } from "../models/index";

export interface ITodoListContext {
  todo: ITodoItem;
  toggleTodo: (todoId: number) => void;
}

export const TodoListContext = createContext<ITodoListContext>({
  todo: null,
  toggleTodo: null
});

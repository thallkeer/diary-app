import { ActionTypes } from "./action-types";
import { ITodoItem } from "../models";
import { TodoActions } from "../actions/todo-actions";
import { getEmptyTodo } from "../utils";
import { TodoState } from ".";

export const todosReducer = (
  state: TodoState,
  { type, payload }: TodoActions
): TodoState => {
  switch (type) {
    case ActionTypes.LOAD_TODOS + ActionTypes.START:
      return { ...state, loading: true };

    case ActionTypes.LOAD_TODOS + ActionTypes.SUCCESS:
      const todos = payload as ITodoItem[];

      return {
        ...state,
        todos: [...todos, getEmptyTodo()],
        loading: false
      };

    case ActionTypes.ADD_TODO: {
      const newTodos = [...state.todos];
      newTodos.splice(state.todos.length - 1, 0, payload as ITodoItem);

      return {
        ...state,
        todos: newTodos
      };
    }

    case ActionTypes.UPDATE_TODO:
      const updatedTodo = payload as ITodoItem;

      return {
        ...state,
        todos: state.todos.map(todo =>
          todo.id === updatedTodo.id ? updatedTodo : todo
        )
      };

    case ActionTypes.TOGGLE_TODO:
      const id = payload;

      return {
        ...state,
        todos: state.todos.map(todo =>
          todo.id === id ? { ...todo, done: !todo.done } : todo
        )
      };

    default:
      return state;
  }
};

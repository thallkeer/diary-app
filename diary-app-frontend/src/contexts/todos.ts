import { ActionTypes } from "./action-types";
import { ITodoItem } from "../models";
import { TodoActions } from "../actions/todo-actions";
import { getEmptyTodo, getRandomId } from "../utils";
import { TodoState } from ".";

export const todosReducer = (
  state: TodoState,
  action: TodoActions
): TodoState => {
  switch (action.type) {
    case ActionTypes.LOAD_TODOS + ActionTypes.START:
      return { ...state, loading: true };

    case ActionTypes.LOAD_TODOS + ActionTypes.SUCCESS:
      const todos = action.payload as ITodoItem[];

      return {
        ...state,
        todos: [...todos, getEmptyTodo()],
        loading: false
      };

    case ActionTypes.UPDATE_TODO: {
      const { todoId, text } = action.payload as {
        todoId: number;
        text: string;
      };

      if (todoId === 0) {
        const newTodo: ITodoItem = {
          id: getRandomId(),
          text: text,
          done: false
        };

        const newTodos = [...state.todos];
        newTodos.splice(state.todos.length - 1, 0, newTodo);

        return {
          ...state,
          todos: newTodos
        };
      }

      return {
        ...state,
        todos: state.todos.map(todo =>
          todo.id === todoId ? { ...todo, text: text } : todo
        )
      };
    }

    case ActionTypes.TOGGLE_TODO:
      const id = action.payload;

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

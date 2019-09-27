import ITodoItem from "../models/todo-model";
import { ActionTypes } from "../actions/action-types";
import * as todoActions from "../actions/todo-actions";

import { getRandomId } from "../utils/index";

export interface ITodoReducerState {
  todos: ITodoItem[];
  loading: boolean;
  loaded: boolean;
}

export const initialState: ITodoReducerState = {
  todos: [],
  loading: false,
  loaded: false
};

export function reducer(
  state: ITodoReducerState = initialState,
  action: todoActions.Actions
): ITodoReducerState {
  switch (action.type) {
    case ActionTypes.ADD_TODO: {
      const { text } = action.payload;
      const newItem: ITodoItem = {
        id: 0,
        text,
        done: false
      };

      return {
        ...state,
        todos: [...state.todos, newItem]
      };
    }

    case ActionTypes.LOAD_TODOS + ActionTypes.START:
      return {
        ...state,
        loading: true,
        loaded: false
      };

    case ActionTypes.LOAD_TODOS + ActionTypes.SUCCESS: {
      const response = action.payload;
      const emptyItem: ITodoItem = {
        id: 0,
        text: "",
        done: false
      };

      return {
        ...state,
        loading: false,
        loaded: true,
        todos: [...response, emptyItem]
      };
    }

    case ActionTypes.UPDATE_TODO: {
      const { todoId, text } = action.payload;
      const newTodos = state.todos;
      const index = newTodos.findIndex(el => el.id === todoId);
      const addNewItem = newTodos[index].text === "";

      newTodos[index].text = text;

      if (addNewItem)
        newTodos.push({ id: getRandomId(), text: "", done: false });

      return {
        ...state,
        todos: newTodos
      };
    }

    case ActionTypes.TOGGLE_TODO: {
      const todoId = action.payload;
      return {
        ...state,
        todos: state.todos.map(todo =>
          todo.id === todoId ? { ...todo, done: !todo.done } : todo
        )
      };
    }

    default:
      return state;
  }
}

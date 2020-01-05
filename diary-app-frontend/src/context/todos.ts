import { ActionTypes } from "./action-types";
import { ITodo, ITodoList } from "../models";
import { TodoActions } from "../actions/todo-actions";
import { getEmptyTodo } from "../utils";
import { getTodos } from "../selectors";
import { ListState } from ".";

export const todosReducer = (
  state: ListState<ITodo>,
  { type, payload }: TodoActions
): ListState<ITodo> => {
  switch (type) {
    case ActionTypes.LOAD_TODOS + ActionTypes.START:
      return { ...state, loading: true };

    case ActionTypes.LOAD_TODOS + ActionTypes.SUCCESS:
      const list = payload as ITodoList;

      let newTodos: ITodoList = {
        ...list,
        items: [...list.items, getEmptyTodo()]
      };

      return {
        ...state,
        list: newTodos,
        loading: false
      };

    case ActionTypes.ADD_TODO: {
      const todoItems = getTodos(state);
      const newTodos = [...todoItems];
      newTodos.splice(todoItems.length - 1, 0, payload as ITodo);

      return {
        ...state,
        list: { ...state.list, items: newTodos }
      };
    }

    case ActionTypes.UPDATE_TODO:
      const updatedTodo = payload as ITodo;

      return {
        ...state,
        list: {
          ...state.list,
          items: state.list.items.map(todo =>
            todo.id === updatedTodo.id ? updatedTodo : todo
          )
        }
      };

    case ActionTypes.TOGGLE_TODO:
      const id = payload;

      return {
        ...state,
        list: {
          ...state.list,
          items: getTodos(state).map(todo =>
            todo.id === id ? { ...todo, done: !todo.done } : todo
          )
        }
      };

    default:
      return state;
  }
};

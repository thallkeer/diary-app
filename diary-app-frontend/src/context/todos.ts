import { ActionTypes } from "./action-types";
import { ITodoItem, IList } from "../models";
import { TodoActions } from "../actions/todo-actions";
import { getEmptyTodo } from "../utils";
import { BaseState } from ".";
import { getTodos } from "../selectors";

export const todosReducer = (
  state: BaseState<ITodoItem>,
  { type, payload }: TodoActions
): BaseState<ITodoItem> => {
  switch (type) {
    case ActionTypes.LOAD_TODOS + ActionTypes.START:
      return { ...state, loading: true };

    case ActionTypes.LOAD_TODOS + ActionTypes.SUCCESS:
      const list = payload as IList<ITodoItem>;

      let newTodos: IList<ITodoItem> = {
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
      newTodos.splice(todoItems.length - 1, 0, payload as ITodoItem);

      return {
        ...state,
        list: { ...state.list, items: newTodos }
      };
    }

    case ActionTypes.UPDATE_TODO:
      const updatedTodo = payload as ITodoItem;

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

import { TodoActions } from "../actions/todo-actions";
import { getTodos } from "../selectors";
import { ITodoListContext } from ".";

export const todosReducer = (
  state: ITodoListContext,
  action: TodoActions
): ITodoListContext => {
  switch (action.type) {
    case "LOAD_TODOS_START":
      return { ...state, loading: true };

    case "LOAD_TODOS":
      return {
        ...state,
        list: action.payload,
        loading: false
      };

    case "ADD_TODO": {
      const todoItems = [...getTodos(state)];
      todoItems.splice(todoItems.length - 1, 0, action.payload);

      return {
        ...state,
        list: { ...state.list, items: todoItems }
      };
    }

    case "UPDATE_TODO":
      return {
        ...state,
        list: {
          ...state.list,
          items: getTodos(state).map(todo =>
            todo.id === action.payload.id ? action.payload : todo
          )
        }
      };

    case "TOGGLE_TODO":
      return {
        ...state,
        list: {
          ...state.list,
          items: getTodos(state).map(todo =>
            todo.id === action.payload ? { ...todo, done: !todo.done } : todo
          )
        }
      };

    default:
      return state;
  }
};

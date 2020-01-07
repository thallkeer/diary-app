import { ITodo } from "../models";
import { TodoActions } from "../actions/todo-actions";
import { getTodos } from "../selectors";
import { ListState } from ".";

export const todosReducer = (
  state: ListState<ITodo>,
  action: TodoActions
): ListState<ITodo> => {
  switch (action.type) {
    case "LOAD_TODOS_START":
      return { ...state, loading: true };

    case "LOAD_TODOS":
      return {
        ...state,
        list: action.payload,
        loading: false
      };

    // case ActionTypes.ADD_TODO: {
    //   const todoItems = getTodos(state);
    //   const newTodos = [...todoItems];
    //   newTodos.splice(todoItems.length - 1, 0, payload as ITodo);

    //   return {
    //     ...state,
    //     list: { ...state.list, items: newTodos }
    //   };
    // }

    case "UPDATE_TODO":
      return {
        ...state,
        list: {
          ...state.list,
          items: state.list.items.map(todo =>
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

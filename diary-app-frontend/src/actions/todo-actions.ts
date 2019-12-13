import { ActionsUnion, createAction } from "./action-helpers";
import { ActionTypes } from "../contexts/action-types";
import { ITodoItem } from "../models";

export const TodoActions = {
  startLoadTodos: () =>
    createAction(ActionTypes.LOAD_TODOS + ActionTypes.START),
  finishLoadTodos: (todos: ITodoItem[]) =>
    createAction(ActionTypes.LOAD_TODOS + ActionTypes.SUCCESS, todos),
  toggleTodo: (todoId: number) => createAction(ActionTypes.TOGGLE_TODO, todoId),
  updateTodo: (todoId: number, subject: string) =>
    createAction<{ todoId: number; text: string }>(ActionTypes.UPDATE_TODO, {
      todoId: todoId,
      text: subject
    })
};

const callApi: string = "https://localhost:44320/api/todo";

export const Thunks = {
  loadTodos: () => {
    return dispatch => {
      dispatch(TodoActions.startLoadTodos());
      fetch(callApi)
        .then(res => res.json())
        .then(response => dispatch(TodoActions.finishLoadTodos(response)));
    };
  },

  toggleTodo: (todoId: number) => {
    return dispatch => {
      dispatch(TodoActions.toggleTodo(todoId));
    };
  },

  addOrUpdateTodo: (todoId: number, subject: string) => {
    return dispatch => {
      dispatch(TodoActions.updateTodo(todoId, subject));
    };
  }
};

export type TodoActions = ActionsUnion<typeof TodoActions>;
export type TodoThunks = typeof Thunks;

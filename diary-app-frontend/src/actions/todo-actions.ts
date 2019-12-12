import { ActionsUnion, createAction } from "./action-helpers";
import { Dispatch } from "redux";
import { ActionTypes } from "../contexts/action-types";

export const TodoActions = {
  startLoadTodos: () =>
    createAction(ActionTypes.LOAD_TODOS + ActionTypes.START),
  finishLoadTodos: (todos: any) =>
    createAction(ActionTypes.LOAD_TODOS + ActionTypes.SUCCESS, todos),
  toggleTodo: (todoId: number) => createAction(ActionTypes.TOGGLE_TODO, todoId),
  updateTodo: (todoId: number, subject: string) =>
    createAction(ActionTypes.UPDATE_TODO, { todoId: todoId, text: subject }),
  addTodo: (subject: string) =>
    createAction(ActionTypes.ADD_TODO, { text: subject })
};

const callApi: string = "https://localhost:44320/api/todo";

export const Thunks = {
  loadTodos: () => {
    return (dispatch: Dispatch) => {
      dispatch(TodoActions.startLoadTodos());
      fetch(callApi)
        .then(res => res.json())
        .then(response => dispatch(TodoActions.finishLoadTodos(response)));
    };
  },

  toggleTodo: (todoId: number) => {
    return (dispatch: Dispatch) => {
      dispatch(TodoActions.toggleTodo(todoId));
    };
  },

  updateTodo: (todoId: number, subject: string) => {
    return (dispatch: Dispatch) => {
      dispatch(TodoActions.updateTodo(todoId, subject));
    };
  },

  addTodo: (subject: string) => {
    return (dispatch: Dispatch) => {
      dispatch(TodoActions.addTodo(subject));
    };
  }
};

export type Actions = ActionsUnion<typeof TodoActions>;

export type Thunks = ActionsUnion<typeof Thunks>;

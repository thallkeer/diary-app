import { ActionsUnion, createAction } from "./action-helpers";
import { ActionTypes } from "../context/action-types";
import { ITodo } from "../models";
import axios from "axios";

export const TodoActions = {
  startLoadTodos: () =>
    createAction(ActionTypes.LOAD_TODOS + ActionTypes.START),
  finishLoadTodos: (todos: ITodo[]) =>
    createAction(ActionTypes.LOAD_TODOS + ActionTypes.SUCCESS, todos),
  toggleTodo: (todoId: number) => createAction(ActionTypes.TOGGLE_TODO, todoId),
  updateTodo: (todo: ITodo) => createAction(ActionTypes.UPDATE_TODO, todo)
};

const callApi: string = "https://localhost:44320/api/todo/";

export const Thunks = {
  loadTodos: (title: string) => {
    return dispatch => {
      dispatch(TodoActions.startLoadTodos());
      axios
        .get(callApi + `${new Date().getMonth() + 1}/title/${title}`)
        .then(res => res.data)
        .then(response => dispatch(TodoActions.finishLoadTodos(response)));
    };
  },

  toggleTodo: (todoId: number) => {
    return dispatch => {
      dispatch(TodoActions.toggleTodo(todoId));
    };
  },

  addOrUpdateTodo: (todo: ITodo) => {
    return dispatch => {
      dispatch(TodoActions.updateTodo(todo));
    };
  }
};

export type TodoActions = ActionsUnion<typeof TodoActions>;
export type TodoThunks = typeof Thunks;

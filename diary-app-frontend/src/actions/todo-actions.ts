import { ActionsUnion, createAction } from "./action-helpers";
import { ITodo, ITodoList } from "../models";
import axios from "axios";

export const ADD_TODO = "ADD_TODO";
export const TOGGLE_TODO = "TOGGLE_TODO";
export const UPDATE_TODO = "UPDATE_TODO";
export const LOAD_TODOS_START = "LOAD_TODOS_START";
export const LOAD_TODOS = "LOAD_TODOS";

export const TodoActions = {
  startLoadTodos: () => createAction(LOAD_TODOS_START),
  finishLoadTodos: (todos: ITodoList) => createAction(LOAD_TODOS, todos),
  toggleTodo: (todoId: number) => createAction(TOGGLE_TODO, todoId),
  updateTodo: (todo: ITodo) => createAction(UPDATE_TODO, todo)
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

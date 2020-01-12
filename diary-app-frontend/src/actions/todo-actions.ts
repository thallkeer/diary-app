import { ActionsUnion, createAction } from "./action-helpers";
import { ITodo, ITodoList } from "../models";
import axios from "axios";

export const ADD_TODO = "ADD_TODO";
export const TOGGLE_TODO = "TOGGLE_TODO";
export const UPDATE_TODO = "UPDATE_TODO";
export const LOAD_TODOS_START = "LOAD_TODOS_START";
export const LOAD_TODOS = "LOAD_TODOS";
export const DELETE_TODO = "DELETE_TODO";

const Actions = {
  startLoadTodos: () => createAction(LOAD_TODOS_START),
  finishLoadTodos: (todos: ITodoList) => createAction(LOAD_TODOS, todos),
  toggleTodo: (todoId: number) => createAction(TOGGLE_TODO, todoId),
  addTodo: (todo: ITodo) => createAction(ADD_TODO, todo),
  updateTodo: (todo: ITodo) => createAction(UPDATE_TODO, todo),
  deleteTodo: (todoId: number) => createAction(DELETE_TODO, todoId)
};

const baseApi: string = "https://localhost:44320/api/todo/";

export const Thunks = {
  loadTodos: (pageID: number) => {
    return dispatch => {
      dispatch(Actions.startLoadTodos());
      axios.get(baseApi + pageID).then(response => {
        dispatch(Actions.finishLoadTodos(response.data));
      });
    };
  },

  toggleTodo: (todoId: number) => {
    return dispatch => {
      dispatch(Actions.toggleTodo(todoId));
    };
  },

  deleteTodo: (todoId: number) => {
    return dispatch => {
      axios
        .delete(baseApi + `${todoId}`)
        .then(dispatch(Actions.deleteTodo(todoId)));
    };
  },

  addOrUpdateTodo: (todo: ITodo) => {
    return dispatch => {
      if (!todo) return;

      console.log("before add or update ", todo);

      if (todo.id === 0) {
        axios.post(baseApi, todo).then(res => {
          console.log("add ", res.data);
          dispatch(Actions.addTodo(res.data));
        });
      } else {
        axios.put(baseApi, todo).then(res => {
          console.log("update ", res.data);
          dispatch(Actions.updateTodo(todo));
        });
      }
    };
  }
};

export type TodoActions = ActionsUnion<typeof Actions>;
export type TodoThunks = ActionsUnion<typeof Thunks>;

import { ActionsUnion, createAction } from "./action-helpers";
import { ITodo, ITodoList } from "../../models";
import axios from "../../axios/axios";

export const ADD_TODO = "ADD_TODO";
export const TOGGLE_TODO = "TOGGLE_TODO";
export const UPDATE_TODO = "UPDATE_TODO";
export const LOAD_TODOS_START = "LOAD_TODOS_START";
export const LOAD_TODOS = "LOAD_TODOS";
export const DELETE_TODO = "DELETE_TODO";
export const UPDATE_TODOLIST = "UPDATE_TODOLIST";

const Actions = {
  startLoadTodos: () => createAction(LOAD_TODOS_START),
  finishLoadTodos: (todos: ITodoList) => createAction(LOAD_TODOS, todos),
  toggleTodo: (todoId: number) => createAction(TOGGLE_TODO, todoId),
  addTodo: (todo: ITodo) => createAction(ADD_TODO, todo),
  updateTodo: (todo: ITodo) => createAction(UPDATE_TODO, todo),
  deleteTodo: (todoId: number) => createAction(DELETE_TODO, todoId),
  updateTodoList: (todoList: ITodoList) =>
    createAction(UPDATE_TODOLIST, todoList),
};

const baseTodoApi: string = `todo/`;

export const Thunks = {
  setTodoList: (todoList: ITodoList) => {
    return (dispatch) => dispatch(Actions.finishLoadTodos(todoList));
  },

  loadTodosByPageID: (pageID: number) => {
    return (dispatch) => {
      dispatch(Actions.startLoadTodos());
      axios
        .get(baseTodoApi + pageID)
        .then((response) => dispatch(Actions.finishLoadTodos(response.data)));
    };
  },

  updateTodoList: (todoList: ITodoList) => {
    return (dispatch) => {
      axios.put(baseTodoApi, todoList);
      dispatch(Actions.updateTodoList(todoList));
    };
  },

  toggleTodo: (todoId: number) => {
    return (dispatch) => {
      axios
        .put(`${baseTodoApi}toggle/${todoId}`, null)
        .then(dispatch(Actions.toggleTodo(todoId)));
    };
  },

  deleteTodo: (todoId: number) => {
    return (dispatch) => {
      axios
        .delete(`${baseTodoApi}deleteTodo/${todoId}`)
        .then(dispatch(Actions.deleteTodo(todoId)));
    };
  },

  addOrUpdateTodo: (todo: ITodo) => {
    return (dispatch) => {
      if (!todo) return;

      if (todo.id === 0) {
        axios.post(baseTodoApi + "addTodo", todo).then((res) => {
          dispatch(Actions.addTodo(res.data));
        });
      } else {
        axios.put(baseTodoApi + "updateTodo", todo).then((res) => {
          dispatch(Actions.updateTodo(todo));
        });
      }
    };
  },
};

export type TodoActions = ActionsUnion<typeof Actions>;
export type TodoThunks = ActionsUnion<typeof Thunks>;

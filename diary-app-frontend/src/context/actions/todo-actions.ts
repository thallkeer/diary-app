import { ActionsUnion } from "./action-helpers";
import { ITodo, ITodoList } from "../../models";
import { getActions } from "./list-actions";

const ADD_TODO = "ADD_TODO";
const TOGGLE_TODO = "TOGGLE_TODO";
const UPDATE_TODO = "UPDATE_TODO";
const LOAD_TODOS_START = "LOAD_TODOS_START";
const LOAD_TODOS = "LOAD_TODOS";
const DELETE_TODO = "DELETE_TODO";
const DELETE_TODOLIST = "DELETE_TODOLIST";
const UPDATE_TODOLIST = "UPDATE_TODOLIST";

const todoListActions = getActions<ITodoList, ITodo>();

export type TodoListActions = ActionsUnion<typeof todoListActions>;

// export const Actions = {
// 	startLoadTodos: () => createAction(LOAD_TODOS_START),
// 	finishLoadTodos: (todos: ITodoList) => createAction(LOAD_TODOS, todos),
// 	toggleTodo: (todoId: number) => createAction(TOGGLE_TODO, todoId),
// 	addTodo: (todo: ITodo) => createAction(ADD_TODO, todo),
// 	updateTodo: (todo: ITodo) => createAction(UPDATE_TODO, todo),
// 	deleteTodo: (todoId: number) => createAction(DELETE_TODO, todoId),
// 	deleteTodoList: (todoListId: number) =>
// 		createAction(DELETE_TODOLIST, todoListId),
// 	updateTodoList: (todoList: ITodoList) =>
// 		createAction(UPDATE_TODOLIST, todoList),
// };

// const baseTodoApi: string = `todo/`;

// export function setTodoList(
// 	todoList: ITodoList,
// 	dispatch: React.Dispatch<TodoActions>
// ) {
// 	dispatch(Actions.finishLoadTodos(todoList));
// }

// export async function loadTodosByPageID(
// 	pageID: number,
// 	dispatch: React.Dispatch<TodoActions>
// ) {
// 	dispatch(Actions.startLoadTodos());
// 	console.log("get todo list by page id ", pageID);
// 	console.trace();
// 	const response = await axios.get(baseTodoApi + pageID);
// 	dispatch(Actions.finishLoadTodos(response.data));
// }

// export async function updateTodoList(
// 	todoList: ITodoList,
// 	dispatch: React.Dispatch<TodoActions>
// ) {
// 	await axios.put(baseTodoApi, todoList);
// 	dispatch(Actions.updateTodoList(todoList));
// }

// export async function toggleTodo(
// 	todoId: number,
// 	dispatch: React.Dispatch<TodoActions>
// ) {
// 	await axios.put(`${baseTodoApi}toggle/${todoId}`, null);
// 	dispatch(Actions.toggleTodo(todoId));
// }

// export async function deleteTodo(
// 	todoId: number,
// 	dispatch: React.Dispatch<TodoActions>
// ) {
// 	await axios.delete(`${baseTodoApi}deleteTodo/${todoId}`);
// 	dispatch(Actions.deleteTodo(todoId));
// }

// export async function deleteTodoList(
// 	todoList: ITodoList,
// 	dispatch: React.Dispatch<TodoActions>
// ) {
// 	await axios.delete(`${baseTodoApi}${todoList.id}`);
// 	dispatch(Actions.deleteTodoList(todoList.id));
// }

// export async function addOrUpdateTodo(
// 	todo: ITodo,
// 	dispatch: React.Dispatch<TodoActions>
// ) {
// 	if (!todo) return;

// 	if (todo.id === 0) {
// 		await axios.post(baseTodoApi + "addTodo", todo).then((res) => {
// 			dispatch(Actions.addTodo({ ...todo, id: res.data }));
// 		});
// 	} else {
// 		await axios.put(baseTodoApi + "updateTodo", todo);
// 		dispatch(Actions.updateTodo(todo));
// 	}
// }

// export type TodoActions = ActionsUnion<typeof Actions>;

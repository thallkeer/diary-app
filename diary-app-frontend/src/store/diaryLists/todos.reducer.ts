import { todosService } from "../../services/todosService";
import {
	createGenericListThunks,
	createGenericSlice,
	generateListSliceReducers,
} from "./lists.reducer";
import { ITodo, ITodoList } from "models";
import { IListState } from "models/states";
import { PayloadAction } from "@reduxjs/toolkit";
import { AppThunk } from "store/store";

export const TOGGLE_TODO = "TODOS/TOGGLE_TODO";

export interface ITodoListState extends IListState<ITodoList, ITodo> {}

export const todoListInitialState: ITodoListState = {
	list: null,
};

const baseReducers = {
	...generateListSliceReducers<ITodoListState, ITodoList, ITodo>(),
	toggleTodo(state: ITodoListState, action: PayloadAction<number>) {
		state.list.items = state.list.items.map((item) =>
			item.id === action.payload ? { ...item, done: !item.done } : item
		);
	},
};

type TodoReducersType = typeof baseReducers;

export const createTodoSlice = (listName: string) =>
	createGenericSlice<ITodoListState, ITodoList, ITodo, TodoReducersType>({
		name: listName,
		initialState: todoListInitialState,
		reducers: {
			...baseReducers,
		},
	});

export type TodoSlice = ReturnType<typeof createTodoSlice>;

export const createTodoListThunks = (slice: TodoSlice) => {
	const baseThunks = createGenericListThunks(
		slice.name,
		todosService.list,
		todosService.items
	);
	const { actions } = slice;
	const toggleTodo =
		(todoId: number): AppThunk =>
		async (dispatch) => {
			if (todoId === 0) return;
			await todosService.items.toggleTodo(todoId);
			dispatch(actions.toggleTodo(todoId));
		};
	return {
		...baseThunks,
		toggleTodo,
	};
};

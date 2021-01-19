import {
	createDiaryListActions,
	getDiaryListActions,
} from "./diaryLists.actions";
import { ActionsUnion, createNamedAction } from "../actions/action-helpers";
import { todosService } from "../../services/todosService";
import { getListItemActions } from "./lists.actions";
import { ITodo, ITodoList } from "models";
import { BaseThunkType } from "store/state.types";

export const TOGGLE_TODO = "TODOS/TOGGLE_TODO";

export const todoActions = {
	...createDiaryListActions<ITodoList, ITodo>(),
	toggleTodo: (todoListName: string, todoId: number) =>
		createNamedAction(TOGGLE_TODO, todoListName, todoId),
};

export const todoThunks = {
	...getDiaryListActions<ITodoList, ITodo>("todoLists"),
	...getListItemActions<ITodo>("todos"),
	toggleTodo: (todoId: number, todoListName: string): ThunkType => async (
		dispatch
	) => {
		await todosService.items.toggleTodo(todoId);
		dispatch(todoActions.toggleTodo(todoListName, todoId));
	},
};

export type TodoActions = ActionsUnion<typeof todoActions>;
type ThunkType = BaseThunkType<TodoActions>;

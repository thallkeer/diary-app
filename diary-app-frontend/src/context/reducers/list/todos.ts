import {
	createDiaryListActions,
	getDiaryListActions,
} from "../../actions/diaryList-actions";
import { ITodo, ITodoList } from "../../../models/entities";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import { ActionsUnion, createNamedAction } from "../../actions/action-helpers";
import { BaseThunkType } from "../../store";
import { createNamedWrapperReducer, updateListInState } from "../../../utils";
import { todosService } from "../../../services/todosService";
import { IDiaryListState } from "../../../models/states";
import { diaryListReducer } from "./diaryListReducer";
import { getListItemActions } from "../../actions/listCrud-actions";

export interface ITodoListState extends IDiaryListState<ITodoList, ITodo> {
	isDeletable: boolean;
	readonlyHeader: boolean;
}

export const todoListInitialState: ITodoListState = {
	list: null,
	isDeletable: false,
	readonlyHeader: false,
	listName: "todolist",
};

export const createTodoListReducer = (reducerName: string) => {
	return createNamedWrapperReducer(
		todosReducer,
		todoListInitialState,
		reducerName
	);
};

export const todosReducer = (
	state = todoListInitialState,
	action: TodoActions
): ITodoListState => {
	switch (action.type) {
		case "TODOS/TOGGLE_TODO":
			return updateListInState(state, (listItems) =>
				listItems.map((item) =>
					item.id === action.payload ? { ...item, done: !item.done } : item
				)
			);

		default:
			return diaryListReducer<ITodoListState, ITodoList, ITodo>(state, action);
	}
};

const TOGGLE_TODO = "TODOS/TOGGLE_TODO";

export const todoActions = {
	...createDiaryListActions<ITodoList, ITodo>(),
	toggleTodo: (todoListName: string, todoId: number) =>
		createNamedAction(TOGGLE_TODO, todoListName, todoId),
};

export const todoActionCreators = {
	...getDiaryListActions<ITodoList, ITodo>("todoLists"),
	...getListItemActions<ITodo>("todos"),
	toggleTodo: (todoId: number, todoListName: string): ThunkType => async (
		dispatch
	) => {
		await todosService.todoService.toggleTodo(todoId);
		dispatch(todoActions.toggleTodo(todoListName, todoId));
	},
};

export type TodoActions = ActionsUnion<typeof todoActions>;
type ThunkType = BaseThunkType<TodoActions>;

import {
	createListActions,
	getListActions,
	ListActions,
	listReducer,
	withListStates,
} from "../../actions/list-actions";
import { IListState, ITodo, ITodoList } from "../../../models";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import { ActionsUnion, createNamedAction } from "../../actions/action-helpers";
import { BaseThunkType, createNamedWrapperReducer } from "../../store";
import { updateListInState } from "../../../utils";
import { todosService } from "../../../services/todosService";

export interface ITodoListState extends IListState<ITodoList, ITodo> {
	isDeletable: boolean;
	readonlyHeader: boolean;
}

const initialState: ITodoListState = {
	list: null,
	isDeletable: false,
	readonlyHeader: false,
	listName: "todolist",
	...INITIAL_LOADABLE_STATE,
};

export const createTodoListReducer = (reducerName: string) => {
	return createNamedWrapperReducer(
		todosReducer,
		initialState,
		reducerName,
		(action) => action.subjectName
	);
};

export const todosReducer = (
	state = initialState,
	action: TodoActions
): ITodoListState => {
	console.log("todos reducer", state, action);

	switch (action.type) {
		case "TODOS/TOGGLE_TODO":
			return updateListInState(state, (listItems) =>
				listItems.map((item) =>
					item.id === action.payload ? { ...item, done: !item.done } : item
				)
			);

		default:
			return listReducer<ITodoListState, ITodoList, ITodo>(state, action);
	}
};

const TOGGLE_TODO = "TODOS/TOGGLE_TODO";

const actions = {
	...createListActions<ITodoList, ITodo>(),
	toggleTodo: (todoListName: string, todoId: number) =>
		createNamedAction(TOGGLE_TODO, todoListName, todoId),
};

export const todoActions = {
	...getListActions<ITodoList, ITodo>("todo/", "Todo"),
	toggleTodo: (todoId: number, todoListName: string): ThunkType => async (
		dispatch
	) => {
		await todosService.toggleTodo(todoId);
		dispatch(actions.toggleTodo(todoListName, todoId));
	},
};

type TodoActions = ActionsUnion<typeof actions>;
type ThunkType = BaseThunkType<TodoActions>;

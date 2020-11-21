import {
	createNamedWrapperListReducer,
	getListActions,
	ListActions,
	withListStates,
} from "../../actions/list-actions";
import { IListState, ITodo, ITodoList } from "../../../models";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import { ActionsUnion, createAction } from "../../actions/action-helpers";
import { BaseThunkType } from "../../store";

export interface ITodoListState extends IListState<ITodoList, ITodo> {
	isDeletable: boolean;
	readonlyHeader: boolean;
}

const initialState: ITodoListState = {
	list: null,
	isDeletable: false,
	readonlyHeader: false,
	listName: 'todolist',
	...INITIAL_LOADABLE_STATE,
};


export const createTodoListReducer = (reducerName: string) => {
	const namedReducer = createNamedWrapperListReducer<ITodoListState, ITodoList, ITodo>(initialState, reducerName);
	const todosWrappedReducer = withListStates()(namedReducer);
	return todosWrappedReducer;
}

export const todosReducer = (
	state = initialState,
	action: TodoActions
): ITodoListState => {
	switch (action.type) {
		case "TODOS/TOGGLE_TODO":
			return { ...state};

		default:
			return state;
	}
};


const TOGGLE_TODO = "TODOS/TOGGLE_TODO";

const actions = {
	toggleTodo: (todoId: number) => createAction(TOGGLE_TODO, todoId),
};

export const todoActions = {
	...getListActions<ITodoList, ITodo>("todo/", "Todo"),
	toggleTodo: (todoId: number): ThunkType => async (dispatch) => {
		dispatch(actions.toggleTodo(todoId));
	},
};

type TodoActions = ActionsUnion<typeof actions> | ListActions;
type ThunkType = BaseThunkType<TodoActions>;

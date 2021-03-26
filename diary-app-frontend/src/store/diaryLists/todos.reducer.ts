import { ActionsUnion, createNamedAction } from "../actions/action-helpers";
import { todosService } from "../../services/todosService";
import { DiaryListComponent } from "./lists.reducer";
import { ITodo, ITodoList } from "models";
import { BaseThunkType } from "store/state.types";
import { IListState } from "models/states";
import { createNamedReducer, updateListInState } from "utils";

export const TOGGLE_TODO = "TODOS/TOGGLE_TODO";

class TodoListComponent {
	private listComponent: DiaryListComponent<ITodoList, ITodo>;

	constructor() {
		this.listComponent = new DiaryListComponent<ITodoList, ITodo>(
			"todoLists",
			"todos"
		);
	}

	public getActions(listName: string) {
		return {
			...this.listComponent.getActions(listName),
			toggleTodo: (todoId: number) =>
				createNamedAction(TOGGLE_TODO, listName, todoId),
		};
	}

	public getThunks(listName: string) {
		const actions = this.getActions(listName);
		type TodoListActions = ActionsUnion<typeof actions>;
		type TodoListThunkType = BaseThunkType<TodoListActions>;

		return {
			...this.listComponent.getThunks(listName),
			toggleTodo: (todoId: number): TodoListThunkType => async (dispatch) => {
				await todosService.items.toggleTodo(todoId);
				dispatch(actions.toggleTodo(todoId));
			},
		};
	}

	public getTodosReducer(initialState: ITodoListState, listName: string) {
		const baseReducer = this.listComponent.getReducer(initialState, listName);
		const actions = this.getActions(listName);
		type TodoListActions = ActionsUnion<typeof actions>;
		const todosReducer = (
			state = initialState,
			action: TodoListActions
		): ITodoListState => {
			switch (action.type) {
				case "TODOS/TOGGLE_TODO":
					return updateListInState(state, (listItems) =>
						listItems.map((item) =>
							item.id === action.payload ? { ...item, done: !item.done } : item
						)
					);

				default:
					return baseReducer(state, action);
			}
		};
		return createNamedReducer(todosReducer, initialState, listName);
	}
}

export const todoListComponent = new TodoListComponent();

const actions = todoListComponent.getActions("todoList");
export type TodoActions = ActionsUnion<typeof actions>;

export interface ITodoListState extends IListState<ITodoList, ITodo> {}

export const todoListInitialState: ITodoListState = {
	list: null,
};

export const createTodoListReducer = (reducerName: string) => {
	return todoListComponent.getTodosReducer(todoListInitialState, reducerName);
};

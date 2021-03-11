import { ITodo, ITodoList } from "models";
import { IDiaryListState } from "models/states";
import { createNamedReducer, updateListInState } from "utils";
import { diaryListReducer } from "./diaryLists.reducer";
import { TodoActions } from "./todos.actions";

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
	return createNamedReducer(todosReducer, todoListInitialState, reducerName);
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

export type TodoReducerType = typeof todosReducer;

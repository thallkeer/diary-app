import { TodoListActions } from "../../actions/todo-actions";
import { ITodoListState } from "../../../components/Lists/TodoList/TodoListState";
import { listReducer } from "../../actions/list-actions";

export const todosReducer = (
	state: ITodoListState,
	action: TodoListActions
): ITodoListState => listReducer(state, action);

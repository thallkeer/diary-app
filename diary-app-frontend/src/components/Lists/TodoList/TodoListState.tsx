import React, { useReducer, createContext } from "react";
import { ITodoList, ITodo, IListState } from "../../../models";
import { todosReducer } from "../../../context/reducers/list/todos";
import { IListContext } from "../../../context";
import { useList } from "../../../hooks/useList";
import { useListActions } from "../../../context/actions/list-actions";
import { getTodos } from "../../../selectors";

type TodoListStateProps = {
	initList?: ITodoList;
	deleteListFunc?: (todoList: ITodoList) => void;
	isDeletable?: boolean;
	readonlyHeader: boolean;
};

export interface ITodoListState extends IListState<ITodoList, ITodo> {}

export interface ITodoListContext extends IListContext {
	todoListState: ITodoListState;
	toggleTodoItem?: (todoId: number) => void;
	deleteTodoList?: (todoList: ITodoList) => void;
	isDeletable: boolean;
	readonlyHeader: boolean;
}

const initialState: ITodoListState = {
	loading: false,
	list: null,
};

const TodoListContext = createContext<ITodoListContext>({
	todoListState: initialState,
	isDeletable: false,
	readonlyHeader: false,
});

const {
	addOrUpdateListItem,
	deleteListItem,
	loadListByPageID,
	setList,
	updateList,
	removeList,
} = useListActions("todo/", "Todo");

const TodoListState: React.FC<TodoListStateProps> = ({
	initList,
	isDeletable = false,
	readonlyHeader = false,
	deleteListFunc,
	children,
}) => {
	const [state, dispatch] = useReducer(todosReducer, {
		loading: false,
		list: initList,
	});

	// useEffect(() => {
	// }, [initList, isDeletable, readonlyHeader, deleteListFunc, children]);

	const todoList = state.list;

	const loadTodoListByPageID = (pageID: number) =>
		loadListByPageID(pageID, dispatch);

	const toggleTodoItem = (todoId: number) => {
		let todoToUpdate = getTodos(state).find((todo) => todo.id === todoId);
		todoId !== 0 &&
			addOrUpdateListItem(
				{
					...todoToUpdate,
					done: !todoToUpdate.done,
				},
				dispatch
			);
	};

	const removeTodoList = (todoList: ITodoList) => {
		if (deleteListFunc !== null) deleteListFunc(todoList);
		removeList(todoList.id, dispatch);
	};

	useList(todoList, loadTodoListByPageID);

	return (
		<TodoListContext.Provider
			value={{
				isDeletable,
				readonlyHeader,
				todoListState: state,
				listFunctions: {
					addOrUpdateItem: (item: ITodo) =>
						addOrUpdateListItem({ ...item, ownerID: todoList.id }, dispatch),
					deleteListItem: (itemID) =>
						itemID !== 0 && deleteListItem(itemID, dispatch),
					loadByPageID: loadTodoListByPageID,
					setList: (list: ITodoList) => setList(list, dispatch),
					updateListTitle: (title) =>
						updateList({ ...todoList, title }, dispatch),
				},
				toggleTodoItem,
				deleteTodoList: removeTodoList,
			}}
		>
			{children}
		</TodoListContext.Provider>
	);
};

export { TodoListContext, TodoListState };

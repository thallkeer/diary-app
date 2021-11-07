import React from "react";
import { ITodoItemActions, TodoInput } from "../Controls/TodoInput";
import { getEmptyTodo, fillToNumber } from "../../../utils";
import {
	ListWithItems,
	IListActions,
	IListOptions,
} from "../CommonList/CommonListComponent";
import { ITodo, ITodoList } from "models";

export interface ITodoListProps extends IListActions, IListOptions {
	todoList: ITodoList;
	className?: string;
	todoItemActions: ITodoItemActions;
}

export const TodoList: React.FC<ITodoListProps> = ({
	todoList,
	updateTitle,
	deleteList,
	readonlyTitle,
	isDeletable,
	todoItemActions,
	className,
}) => {
	const { updateTodo, deleteTodo, toggleTodo } = todoItemActions;
	const todos = fillToNumber(todoList.items, todoList.items.length + 1, () =>
		getEmptyTodo(todoList.id)
	);

	return (
		<ListWithItems
			className={`mt-52 ${className}`}
			items={todos}
			listTitle={todoList.title}
			readonlyTitle={readonlyTitle}
			updateTitle={updateTitle}
			isDeletable={isDeletable}
			deleteList={deleteList}
			renderItem={(todo: ITodo) => (
				<TodoInput
					todo={todo}
					updateTodo={updateTodo}
					toggleTodo={toggleTodo}
					deleteTodo={deleteTodo}
				/>
			)}
		/>
	);
};

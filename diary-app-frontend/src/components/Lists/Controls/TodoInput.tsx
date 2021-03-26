import React, { FC } from "react";
import { CheckCircle } from "./CheckCircle";
import { ListItemInput } from "./ListItemInput";
import { ITodo } from "models";
import { withItemContextMenu } from "../CommonList/CommonListComponent";
import { useUrlInput } from "hooks/useInputs";

export interface ITodoItemActions {
	toggleTodo: (todoId: number) => void;
	updateTodo: (todo: ITodo) => void;
	deleteTodo: (todoID: number) => void;
}

interface ITodoInputProps extends ITodoItemActions {
	todo: ITodo;
}

export const TodoInput: FC<ITodoInputProps> = ({
	updateTodo,
	toggleTodo,
	deleteTodo,
	todo,
}) => {
	const { editUrlMode, urlInput, menuItem } = useUrlInput({
		item: todo,
		updateItem: updateTodo,
	});

	const handleDeleteClick = () => {
		deleteTodo(todo.id);
	};

	const todoInput = (
		<span className="todo-input">
			<CheckCircle id={todo.id} done={todo.done} toggleTodo={toggleTodo} />
			{editUrlMode ? (
				urlInput
			) : (
				<ListItemInput
					item={todo}
					updateItem={updateTodo}
					readonly={todo.readonly}
				/>
			)}
		</span>
	);

	return withItemContextMenu(todoInput, todo.id, handleDeleteClick, [menuItem]);
};

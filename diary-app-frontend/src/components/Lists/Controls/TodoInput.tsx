import React, { FC } from "react";
import { CheckCircle } from "./CheckCircle";
import { ListItemInput } from "./ListItemInput";
import { ITodo } from "models";
import { WithUrlEdit } from "../CommonList/CommonList";

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
	const handleDeleteClick = () => {
		deleteTodo(todo.id);
	};

	return (
		<span className="todo-input">
			{todo.id !== 0 && (
				<CheckCircle id={todo.id} done={todo.done} toggleTodo={toggleTodo} />
			)}
			<WithUrlEdit
				inputElement={
					<ListItemInput
						item={todo}
						updateItem={updateTodo}
						readonly={todo.readonly}
					/>
				}
				item={todo}
				listItemActions={{
					updateItem: updateTodo,
					deleteItem: handleDeleteClick,
				}}
			/>
		</span>
	);
};

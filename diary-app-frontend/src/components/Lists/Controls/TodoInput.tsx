import React, { FC, useState, useEffect } from "react";
import { CheckCircle } from "./CheckCircle";
import { ListItemInput, UrlInput } from "./ListItemInput";
import { ITodo } from "../../../models";
import { MenuItem } from "react-contextmenu";
import { withContextMenu } from "../CommonList/CommonListComponent";

interface ITodoInputProps {
	todo: ITodo;
	toggleTodo: (todoId: number) => void;
	updateTodo: (todo: ITodo) => void;
	deleteTodo: (todoID: number) => void;
}

export const TodoInput: FC<ITodoInputProps> = ({
	updateTodo,
	toggleTodo,
	deleteTodo,
	todo,
}) => {
	const [editUrlMode, setEditUrlMode] = useState(false);

	const handleAddClick = () => {
		setEditUrlMode(true);
	};

	const handleDeleteClick = () => {
		deleteTodo(todo.id);
	};

	const updateTodoItem = (todoItem: ITodo) => {
		updateTodo(todoItem);
		setEditUrlMode(false);
	};

	useEffect(() => {}, [todo, updateTodo, deleteTodo, toggleTodo, editUrlMode]);

	const todoInput = (
		<>
			<CheckCircle id={todo.id} done={todo.done} toggleTodo={toggleTodo} />
			{editUrlMode ? (
				<UrlInput
					item={todo}
					updateItem={updateTodoItem}
					endEdit={() => setEditUrlMode(false)}
				/>
			) : (
				<ListItemInput
					item={todo}
					updateItem={updateTodoItem}
					readonly={todo.readonly}
				/>
			)}
		</>
	);

	if (todo.id === 0) return todoInput;

	return withContextMenu(todoInput, todo.id, handleDeleteClick, [
		<MenuItem key="menu-item-add" onClick={handleAddClick} className="menuItem">
			Добавить/Изменить гиперссылку
		</MenuItem>,
	]);
};

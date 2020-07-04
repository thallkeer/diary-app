import React, { useContext } from "react";
import { TodoInput } from "../Controls/TodoInput";
import Loader from "../../Loader";
import { getEmptyTodo, fillToNumber } from "../../../utils";
import { TodoListContext } from "./TodoListState";
import { CommonListComponent } from "../CommonList/CommonListComponent";
import { ITodo } from "../../../models";

export const TodoList: React.FC<React.HtmlHTMLAttributes<HTMLDivElement>> = ({
	className,
}) => {
	const {
		todoListState,
		toggleTodoItem,
		deleteTodoList,
		isDeletable,
		readonlyHeader,
		listFunctions,
	} = useContext(TodoListContext);

	const { updateListTitle, addOrUpdateItem, deleteListItem } = listFunctions;

	const { list, loading } = todoListState;

	if (loading || !list) return <Loader />;

	const todos = fillToNumber([...list.items], 6, getEmptyTodo);

	return (
		<CommonListComponent
			className={`mt-52 ${className}`}
			items={todos}
			listTitle={list.title}
			readonlyTitle={readonlyHeader}
			updateListTitle={updateListTitle}
			isDeletable={isDeletable}
			onDeleteList={() => deleteTodoList(list)}
			renderItem={(todo: ITodo) => (
				<TodoInput
					todo={todo}
					updateTodo={addOrUpdateItem}
					toggleTodo={toggleTodoItem}
					deleteTodo={deleteListItem}
				/>
			)}
		/>
	);
};

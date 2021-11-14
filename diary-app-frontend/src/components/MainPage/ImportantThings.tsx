import React from "react";
import { useSelector } from "react-redux";
import {
	getImportantThingsArea,
	getImportantThingsList,
} from "../../selectors/pages.selectors";
import Loader from "../Loader";
import { TodoList } from "../Lists/TodoList/TodoList";
import { ITodoItemActions } from "../Lists/Controls/TodoInput";
import {
	importantThingsThunks,
	loadImportantThingsArea,
} from "store/pageAreas/importantThingsArea.reducer";
import { useMainPageArea } from "hooks/usePageArea";
import { useAppDispatch } from "hooks/hooks";

const ImportantThingsArea: React.FC = () => {
	const dispatch = useAppDispatch();
	const { status } = useMainPageArea(
		getImportantThingsArea,
		loadImportantThingsArea
	);

	const { list } = useSelector(getImportantThingsList);

	if (status !== "succeeded" || !list) return <Loader />;

	const todoItemActions: ITodoItemActions = {
		deleteTodo: (todoId) =>
			dispatch(importantThingsThunks.deleteItemById(todoId)),
		toggleTodo: (todoId) => dispatch(importantThingsThunks.toggleTodo(todoId)),
		updateTodo: (todo) => dispatch(importantThingsThunks.addOrUpdateItem(todo)),
	};

	return (
		<TodoList
			className="important-things no-list-header"
			isDeletable={false}
			readonlyTitle={true}
			todoList={list}
			todoItemActions={todoItemActions}
		/>
	);
};

export default ImportantThingsArea;

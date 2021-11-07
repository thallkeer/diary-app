import React from "react";
import { useSelector } from "react-redux";
import { Row, Col } from "react-bootstrap";
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
		<Row>
			<Col md={12}>
				<TodoList
					className="mt-10 no-list-header"
					isDeletable={false}
					readonlyTitle={true}
					todoList={list}
					todoItemActions={todoItemActions}
				/>
			</Col>
		</Row>
	);
};

export default ImportantThingsArea;

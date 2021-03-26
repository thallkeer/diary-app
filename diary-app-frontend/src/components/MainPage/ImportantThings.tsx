import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Row, Col } from "react-bootstrap";
import {
	getImportantThingsArea,
	getImportantThingsList,
} from "../../store/pages/pages.selectors";
import Loader from "../Loader";
import { TodoList } from "../Lists/TodoList/TodoList";
import { ITodoItemActions } from "../Lists/Controls/TodoInput";
import {
	importantThingsAreaComponent,
	importantThingsThunks,
} from "store/pageAreas/importantThingsArea.reducer";
import { useMainPageArea } from "hooks/usePageArea";

const ImportantThingsArea: React.FC = () => {
	const dispatch = useDispatch();
	const { isLoading } = useMainPageArea(
		getImportantThingsArea,
		importantThingsAreaComponent
	);

	const { list } = useSelector(getImportantThingsList);

	if (isLoading || !list) return <Loader />;

	const todoItemActions: ITodoItemActions = {
		deleteTodo: (todoId) =>
			dispatch(importantThingsThunks.deleteListItem(todoId)),
		toggleTodo: (todoId) => dispatch(importantThingsThunks.toggleTodo(todoId)),
		updateTodo: (todo) =>
			dispatch(importantThingsThunks.addOrUpdateListItem(todo)),
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

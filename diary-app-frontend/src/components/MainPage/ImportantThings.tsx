import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Row, Col } from "react-bootstrap";
import {
	getImportantThingsList,
	getMainPage,
} from "../../store/pages/pages.selectors";
import Loader from "../Loader";
import { TodoList } from "../Lists/TodoList/TodoList";
import { ITodoItemActions } from "../Lists/Controls/TodoInput";
import { todoThunks } from "../../store/diaryLists/todos.actions";
import { getAppInfo } from "../../selectors/app-selectors";
import {
	IMPORTANT_THINGS_LIST,
	loadImportantThingsArea,
} from "store/pageAreas/importantThings/importantThingsArea.actions";

const ImportantThingsArea: React.FC = () => {
	const dispatch = useDispatch();
	const mainPage = useSelector(getMainPage);
	const { user, year, month } = useSelector(getAppInfo);
	const { list } = useSelector(getImportantThingsList);

	useEffect(() => {
		if (mainPage) {
			dispatch(loadImportantThingsArea(mainPage.id));
		}
	}, [mainPage, user, year, month]);

	if (!mainPage || !list) return <Loader />;

	const todoItemActions: ITodoItemActions = {
		deleteTodo: (todoId) =>
			dispatch(todoThunks.deleteListItem(todoId, IMPORTANT_THINGS_LIST)),
		toggleTodo: (todoId) =>
			dispatch(todoThunks.toggleTodo(todoId, IMPORTANT_THINGS_LIST)),
		updateTodo: (todo) =>
			dispatch(todoThunks.addOrUpdateListItem(todo, IMPORTANT_THINGS_LIST)),
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

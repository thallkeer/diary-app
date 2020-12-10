import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Row, Col } from "react-bootstrap";
import {
	IMPORTANT_THINGS_LIST,
	loadImportantThingsArea,
} from "../../context/reducers/pageArea/importantThingsArea-reducer";
import {
	getImportantThingsList,
	getMainPage,
} from "../../selectors/page-selectors";
import Loader from "../Loader";
import { TodoList } from "../Lists/TodoList/TodoList";
import { ITodoItemActions } from "../Lists/Controls/TodoInput";
import { todoActions } from "../../context/reducers/list/todos";
import { getAppInfo } from "../../selectors/app-selectors";

const ImportantThingsArea: React.FC = () => {
	const dispatch = useDispatch();
	const mainPage = useSelector(getMainPage);
	const { user, year, month } = useSelector(getAppInfo);
	const { isLoading, list } = useSelector(getImportantThingsList);

	useEffect(() => {
		if (mainPage) {
			dispatch(loadImportantThingsArea(mainPage.id));
		}
	}, [mainPage, user, year, month]);

	if (isLoading || !mainPage || !list) return <Loader />;

	const todoItemActions: ITodoItemActions = {
		deleteTodo: (todoId) =>
			dispatch(todoActions.deleteListItem(todoId, IMPORTANT_THINGS_LIST)),
		toggleTodo: (todoId) =>
			dispatch(todoActions.toggleTodo(todoId, IMPORTANT_THINGS_LIST)),
		updateTodo: (todo) =>
			dispatch(todoActions.addOrUpdateListItem(todo, IMPORTANT_THINGS_LIST)),
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

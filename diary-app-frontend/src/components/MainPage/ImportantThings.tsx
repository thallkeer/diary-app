import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Row, Col } from "react-bootstrap";
import { loadImportantThingsArea } from "../../context/reducers/pageArea/importantThingsArea-reducer";
import {
	getImportantThingsArea,
	getImportantThingsList,
	getMainPage,
} from "../../selectors/page-selectors";
import Loader from "../Loader";
import { TodoList } from "../Lists/TodoList/TodoList";
import { ITodoItemActions } from "../Lists/Controls/TodoInput";
import { todoActions } from "../../context/reducers/list/todos";

const ImportantThingsArea: React.FC = () => {
	const dispatch = useDispatch();
	const mainPage = useSelector(getMainPage);
	const { area } = useSelector(getImportantThingsArea);
	const { isLoading, list } = useSelector(getImportantThingsList);

	useEffect(() => {
		if (mainPage && !list) {
			dispatch(loadImportantThingsArea(mainPage.id));
		}
	}, [mainPage, list]);

	if (isLoading || !mainPage || !list) return <Loader />;

	const todoItemActions: ITodoItemActions = {
		deleteTodo: (todoId) => dispatch(todoActions.deleteListItem(todoId)),
		toggleTodo: (todoId) => dispatch(todoActions.toggleTodo(todoId)),
		updateTodo: (todo) => dispatch(todoActions.addOrUpdateListItem(todo)),
	};

	return (
		<>
			<h1 className="area-header">{area?.header}</h1>
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
		</>
	);
};

export default ImportantThingsArea;

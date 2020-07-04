import React, { useContext, useEffect } from "react";
import { TodoList } from "../Lists/TodoList/TodoList";
import {
	TodoListState,
	TodoListContext,
} from "../Lists/TodoList/TodoListState";
import { mainPageContext } from "./MainPageState";
import { Actions } from "../../context/actions/mainPage-actions";
import Loader from "../Loader";

const ImportantThings = () => {
	const mpContext = useContext(mainPageContext);
	const { mainPage, loading } = mpContext.state;

	if (loading || !mainPage || !mainPage.page) return <Loader />;

	return (
		<TodoListState readonlyHeader={true} isDeletable={false}>
			<ImportantThingsList />
		</TodoListState>
	);
};

export const ImportantThingsList: React.FC = () => {
	const pageState = useContext(mainPageContext);
	const { dispatch } = pageState;
	const todoListState = useContext(TodoListContext).todoListState;

	useEffect(() => {
		const { list, loading } = todoListState;

		if (list && !loading) dispatch(Actions.setTodos(todoListState));
	}, [todoListState, dispatch]);

	return <TodoList />;
};

export default ImportantThings;

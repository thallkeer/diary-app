import React, { useContext, useEffect } from "react";
import { ITodo, ITodoList } from "../../models";
import { Row, Col } from "react-bootstrap";
import { AddListBtn } from "../AddListBtn";
import Loader from "../Loader";
import { getSelectedPage } from "../../selectors";
import { useDispatch, useSelector } from "react-redux";
import {
	getMainPage,
	getMonthPage,
	getPurchasesArea,
} from "../../selectors/page-selectors";
import {
	addPurchasesList,
	loadPurchasesArea,
	PURCHASES_LIST,
} from "../../context/reducers/pageArea/purchasesArea-reducer";
import { getAppInfo } from "../../selectors/app-selectors";
import { TodoList } from "../Lists/TodoList/TodoList";
import { todoActions } from "../../context/reducers/list/todos";

type ListPair = {
	list1: ITodoList;
	list2: ITodoList;
};

const PurchasesArea = () => {
	const dispatch = useDispatch();
	const monthPage = useSelector(getMonthPage);
	const app = useSelector(getAppInfo);
	const { user, year, month } = app;
	const { area, isLoading } = useSelector(getPurchasesArea);
	const selectedPage = useSelector(getSelectedPage);

	useEffect(() => {
		console.log("purchases area effect", selectedPage);

		if (monthPage) {
			console.log("load purchases area");
			dispatch(loadPurchasesArea(monthPage.id));
		}
	}, [monthPage, user, year, month]);

	if (isLoading || !monthPage || !area) return <Loader />;

	const getTodoItemActions = (listId: number) => {
		const listName = `${PURCHASES_LIST}_${listId}`;

		return {
			deleteTodo: (todoId: number) =>
				dispatch(todoActions.deleteListItem(todoId, listName)),
			toggleTodo: (todoId: number) =>
				dispatch(todoActions.toggleTodo(todoId, listName)),
			updateTodo: (todo: ITodo) =>
				dispatch(todoActions.addOrUpdateListItem(todo, listName)),
		};
	};

	const getRow = (pair: ListPair) => {
		return (
			<Row key={pair.list1.id}>
				<TodoList
					className="mt-20 month-lists-header"
					isDeletable={true}
					readonlyTitle={false}
					todoItemActions={getTodoItemActions(pair.list1.id)}
					todoList={pair.list1}
				/>

				{pair.list2 && (
					<TodoList
						className="mt-20 month-lists-header"
						isDeletable={true}
						readonlyTitle={false}
						todoItemActions={getTodoItemActions(pair.list2.id)}
						todoList={pair.list2}
					/>
				)}
			</Row>
		);
	};

	const renderLists = (todoLists: ITodoList[]) => {
		const rows = [];

		if (todoLists.length === 2) {
			rows.push(getRow({ list1: todoLists[0], list2: todoLists[1] }));
		} else {
			for (let i = 0; i < todoLists.length - 1; i += 2) {
				rows.push(getRow({ list1: todoLists[i], list2: todoLists[i + 1] }));
			}

			if (todoLists.length % 2 !== 0) {
				rows.push(
					getRow({ list1: todoLists[todoLists.length - 1], list2: null })
				);
			}
		}

		return rows;
	};

	const addList = () => {
		//TODO: добавить идентификатор зоны, раньше он сюда передавался
		const todoList: ITodoList = {
			id: 0,
			items: [],
			pageID: selectedPage.id,
			title: "Список покупок",
		};
		dispatch(addPurchasesList(todoList));
	};

	return (
		<>
			<h1 className="area-header">{area.header}</h1>
			{renderLists(area.purchasesLists)}
			<Row>
				<AddListBtn onClick={addList} />
			</Row>
		</>
	);
};

export default PurchasesArea;

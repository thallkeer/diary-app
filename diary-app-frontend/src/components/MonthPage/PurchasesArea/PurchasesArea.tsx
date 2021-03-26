import React from "react";
import { Row, Col } from "react-bootstrap";
import { AddListBtn } from "../../AddListBtn";
import Loader from "../../Loader";
import { useDispatch, useSelector } from "react-redux";
import {
	getPurchaseLists,
	getPurchasesArea,
} from "../../../store/pages/pages.selectors";

import { TodoList } from "../../Lists/TodoList/TodoList";
import { useMonthPageArea } from "../../../hooks/usePageArea";
import { purchaseListsHandler } from "store/pageAreaLists/purchasesLists/purchaseLists.reducer";
import { purchasesAreaComponent } from "store/pageAreas/purchasesArea.reducer";
import { ITodo, ITodoList } from "models";
import { purchaseListsThunks } from "store/pageAreaLists/purchasesLists/purchaseLists.actions";
import { todoListComponent } from "store/diaryLists";

type ListPair = {
	list1: ITodoList;
	list2: ITodoList;
};

const PurchasesArea = () => {
	const dispatch = useDispatch();
	const { area, isLoading } = useMonthPageArea(
		getPurchasesArea,
		purchasesAreaComponent
	);
	const purchaseLists = useSelector(getPurchaseLists);

	if (isLoading) return <Loader />;

	const getRow = (pair: ListPair) => {
		return (
			<Row key={`${pair.list1.id}_${pair.list2?.id}`}>
				<PurchaseList purchaseList={pair.list1} />
				{pair.list2 && <PurchaseList purchaseList={pair.list2} />}
			</Row>
		);
	};

	const renderLists = (purchaseLists: ITodoList[]) => {
		const rows = [];

		for (let i = 0; i < purchaseLists.length - 1; i += 2) {
			rows.push(
				getRow({
					list1: purchaseLists[i],
					list2: purchaseLists[i + 1],
				})
			);
		}

		if (purchaseLists.length % 2 !== 0) {
			rows.push(
				getRow({
					list1: purchaseLists[purchaseLists.length - 1],
					list2: null,
				})
			);
		}

		return rows;
	};

	const addList = () => {
		const todoList: ITodoList = {
			id: 0,
			items: [],
			title: "Список покупок",
		};
		dispatch(purchaseListsThunks.addPurchaseList(todoList, area.id));
	};

	return (
		<>
			<h1 className="area-header">{area.header}</h1>
			{renderLists(purchaseLists.map((pl) => pl.list))}
			<Row>
				<AddListBtn onClick={addList} />
			</Row>
		</>
	);
};

const PurchaseList: React.FC<{ purchaseList: ITodoList }> = ({
	purchaseList,
}) => {
	const { updateTitle, deleteList, todoItemActions } = usePurchaseList(
		purchaseList
	);

	return (
		<Col md={6}>
			<TodoList
				className="mt-20 month-lists-header"
				isDeletable={true}
				readonlyTitle={false}
				deleteList={deleteList}
				updateTitle={updateTitle}
				todoItemActions={todoItemActions}
				todoList={purchaseList}
			/>
		</Col>
	);
};

const usePurchaseList = (purchaseList: ITodoList) => {
	const dispatch = useDispatch();
	const listName = purchaseListsHandler.getListName(purchaseList.id);
	const todoThunks = todoListComponent.getThunks(listName);

	const deleteList = () => {
		dispatch(purchaseListsThunks.deletePurchaseList(purchaseList.id));
	};

	const updateTitle = (title: string) => {
		dispatch(
			todoThunks.updateList({
				...purchaseList,
				title,
			})
		);
	};

	const todoItemActions = {
		deleteTodo: (todoId: number) => dispatch(todoThunks.deleteListItem(todoId)),
		toggleTodo: (todoId: number) => dispatch(todoThunks.toggleTodo(todoId)),
		updateTodo: (todo: ITodo) => dispatch(todoThunks.addOrUpdateListItem(todo)),
	};

	return {
		updateTitle,
		deleteList,
		todoItemActions,
	};
};

export { PurchasesArea };

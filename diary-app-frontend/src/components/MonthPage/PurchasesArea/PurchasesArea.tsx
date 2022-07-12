import React from "react";
import { Row, Col, Accordion } from "react-bootstrap";
import { loadPurchasesArea } from "store/pageAreas/purchasesArea.reducer";
import { ITodo, ITodoList } from "models";
import { useAppDispatch } from "hooks/hooks";
import { useMonthPageArea } from "hooks/usePageArea";
import { useSelector } from "react-redux";
import Loader from "components/Loader";
import { getPurchaseLists, getPurchasesArea } from "selectors/pages.selectors";
import {
	addPurchaseList,
	setPurchaseLists,
	updatePurchaseList,
	deletePurchaseItem,
	addOrUpdatePurchaseItem,
	deletePurchaseList,
	togglePurchaseItem,
} from "store/pageAreaLists/purchaseLists.slice";
import { AddListBtn } from "components/AddListBtn";
import { TodoList } from "components/Lists/TodoList/TodoList";
import { useState } from "react";
import { WithItemContextMenu } from "components/Lists/CommonList/CommonListComponent";

type ListPair = {
	list1: ITodoList;
	list2: ITodoList;
};

const PurchasesArea = () => {
	const dispatch = useAppDispatch();
	const { area, status } = useMonthPageArea(
		getPurchasesArea,
		loadPurchasesArea
	);
	const purchaseLists = useSelector(getPurchaseLists);

	if (status === "idle" || status === "loading") return <Loader />;

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
		dispatch(addPurchaseList(todoList, area.id));
	};

	return (
		<>
			<h1 className="area-header">{area.header}</h1>
			{renderLists(purchaseLists.map((pl) => pl.list))}
			<AddListBtn onClick={addList} />
		</>
	);
};

const PurchaseList: React.FC<{ purchaseList: ITodoList }> = ({
	purchaseList,
}) => {
	const { updateTitle, deleteList, todoItemActions } =
		usePurchaseList(purchaseList);

	return (
		<Col md={12} className="mt-20">
			<Accordion defaultActiveKey="0">
				<Accordion.Item eventKey={purchaseList.id.toString()}>
					<WithItemContextMenu itemId={purchaseList.id} onDelete={deleteList}>
						<Accordion.Header>{purchaseList.title}</Accordion.Header>
					</WithItemContextMenu>
					<Accordion.Body>
						<TodoList
							className="mt-1 month-lists-header"
							isDeletable={true}
							readonlyTitle={false}
							deleteList={deleteList}
							updateTitle={updateTitle}
							todoItemActions={todoItemActions}
							todoList={purchaseList}
						/>
					</Accordion.Body>
				</Accordion.Item>
			</Accordion>
		</Col>
	);
};

const usePurchaseList = (purchaseList: ITodoList) => {
	const dispatch = useAppDispatch();
	const listId = purchaseList.id;

	const deleteList = () => {
		dispatch(deletePurchaseList(purchaseList.id));
	};

	const updateTitle = (title: string) => {
		dispatch(
			updatePurchaseList({
				...purchaseList,
				title,
			})
		);
	};

	const todoItemActions = {
		deleteTodo: (todoId: number) =>
			dispatch(deletePurchaseItem(listId, todoId)),
		toggleTodo: (todoId: number) =>
			dispatch(togglePurchaseItem(listId, todoId)),
		updateTodo: (todo: ITodo) =>
			dispatch(addOrUpdatePurchaseItem(listId, todo)),
	};

	return {
		updateTitle,
		deleteList,
		todoItemActions,
	};
};

export { PurchasesArea };

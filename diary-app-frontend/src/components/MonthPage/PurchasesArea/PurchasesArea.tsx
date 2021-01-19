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
import { usePageArea } from "../../../hooks/usePageArea";
import {
	IPurchaseListState,
	purchaseListsHandler,
} from "store/pageAreaLists/purchasesLists/purchaseLists.reducer";
import { IPurchasesAreaState } from "store/pageAreas/purchases/purchasesArea.reducer";
import { IPurchasesArea } from "models/PageAreas/pageAreas";
import { loadPurchasesArea } from "store/pageAreas/purchases/purchasesArea.actions";
import { ITodo, ITodoList } from "models";
import { purchaseListsThunks } from "store/pageAreaLists/purchasesLists/purchaseLists.actions";

type ListPair = {
	list1: IPurchaseListState;
	list2: IPurchaseListState;
};

const PurchasesArea = () => {
	const dispatch = useDispatch();
	const { area, isLoading, monthPage } = usePageArea<
		IPurchasesAreaState,
		IPurchasesArea
	>(getPurchasesArea, (dispatch, pageId) => {
		dispatch(loadPurchasesArea(pageId));
	});
	const purchaseLists = useSelector(getPurchaseLists);

	if (isLoading) return <Loader />;

	const getRow = (pair: ListPair) => {
		return (
			<Row key={`${pair.list1.purchaseListId}_${pair.list2?.purchaseListId}`}>
				<PurchaseList purchaseList={pair.list1} />
				{pair.list2 && <PurchaseList purchaseList={pair.list2} />}
			</Row>
		);
	};

	const renderLists = (purchaseLists: IPurchaseListState[]) => {
		const rows = [];

		if (purchaseLists.length === 2) {
			rows.push(getRow({ list1: purchaseLists[0], list2: purchaseLists[1] }));
		} else {
			for (let i = 0; i < purchaseLists.length - 1; i += 2) {
				rows.push(
					getRow({ list1: purchaseLists[i], list2: purchaseLists[i + 1] })
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
		}

		return rows;
	};

	const addList = () => {
		const todoList: ITodoList = {
			id: 0,
			items: [],
			pageId: monthPage.id,
			title: "Список покупок",
		};
		dispatch(
			purchaseListsThunks.addPurchaseList({
				id: 0,
				list: todoList,
				areaOwnerId: area.id,
			})
		);
	};

	return (
		<>
			<h1 className="area-header">{area.header}</h1>
			{renderLists(purchaseLists)}
			<Row>
				<AddListBtn onClick={addList} />
			</Row>
		</>
	);
};

export default PurchasesArea;

const PurchaseList: React.FC<{ purchaseList: IPurchaseListState }> = ({
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
				todoList={purchaseList.listState.list}
			/>
		</Col>
	);
};

const usePurchaseList = (purchaseList: IPurchaseListState) => {
	const dispatch = useDispatch();
	const listName = purchaseListsHandler.getListName(
		purchaseList.purchaseListId
	);

	const deleteList = () => {
		dispatch(
			purchaseListsThunks.deletePurchaseList(purchaseList.purchaseListId)
		);
	};

	const updateTitle = (title: string) => {
		dispatch(
			purchaseListsThunks.updateList(
				{
					...purchaseList.listState.list,
					title,
				},
				listName
			)
		);
	};

	const todoItemActions = {
		deleteTodo: (todoId: number) =>
			dispatch(purchaseListsThunks.deleteListItem(todoId, listName)),
		toggleTodo: (todoId: number) =>
			dispatch(purchaseListsThunks.toggleTodo(todoId, listName)),
		updateTodo: (todo: ITodo) =>
			dispatch(purchaseListsThunks.addOrUpdateListItem(todo, listName)),
	};

	return {
		updateTitle,
		deleteList,
		todoItemActions,
	};
};

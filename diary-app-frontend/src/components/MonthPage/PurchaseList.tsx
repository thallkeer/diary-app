import React, { useContext } from "react";
import { ITodoList } from "../../models";
import { Col } from "react-bootstrap";
import { TodoList } from "../Lists/TodoList/TodoList";
import { TodoListState } from "../Lists/TodoList/TodoListState";
import { purchasesAreaContext } from "./PurchasesAreaState";
import { deletePurchasesList } from "../../context/actions/purchasesArea-actions";

const PurchaseList: React.FC<{ todoList: ITodoList }> = ({ todoList }) => {
	const { dispatch } = useContext(purchasesAreaContext);

	const deleteList = (todoList: ITodoList) =>
		deletePurchasesList(todoList, dispatch);

	return (
		<Col md={6}>
			<TodoListState
				readonlyHeader={false}
				initList={todoList}
				deleteListFunc={deleteList}
				isDeletable={true}
			>
				<TodoList className="mt-20 month-lists-header" />
			</TodoListState>
		</Col>
	);
};

export default PurchaseList;

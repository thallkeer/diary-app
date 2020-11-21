import React, { useContext } from "react";
// import { ITodoList } from "../../models";
// import { Row, Col } from "react-bootstrap";
// import { AddListBtn } from "../AddListBtn";
// import { purchasesAreaContext, PurchasesAreaState } from "./PurchasesAreaState";
// import Loader from "../Loader";
// import { store } from "../../context/store";
// import PurchaseList from "./PurchaseList";
// import { addPurchasesList } from "../../context/actions/purchasesArea-actions";
// import { getSelectedPage } from "../../selectors";

// type ListPair = {
// 	list1: ITodoList;
// 	list2: ITodoList;
// };

// const PurchasesAreaLists = () => {
// 	const { purchasesAreaState, dispatch } = useContext(purchasesAreaContext);
// 	const { area, loading } = purchasesAreaState;
// 	const app = useContext(store).state;

// 	if (!area || loading) return <Loader />;

// 	const getRow = (pair: ListPair) => {
// 		return (
// 			<Row key={pair.list1.id}>
// 				{<PurchaseList todoList={pair.list1} />}
// 				{pair.list2 && <PurchaseList todoList={pair.list2} />}
// 			</Row>
// 		);
// 	};

// 	const renderLists = (todoLists: ITodoList[]) => {
// 		const rows = [];

// 		if (todoLists.length === 2) {
// 			rows.push(getRow({ list1: todoLists[0], list2: todoLists[1] }));
// 		} else {
// 			for (let i = 0; i < todoLists.length - 1; i += 2) {
// 				rows.push(getRow({ list1: todoLists[i], list2: todoLists[i + 1] }));
// 			}

// 			if (todoLists.length % 2 !== 0) {
// 				rows.push(
// 					getRow({ list1: todoLists[todoLists.length - 1], list2: null })
// 				);
// 			}
// 		}

// 		return rows;
// 	};

// 	const addList = () => {
// 		const selectedPage = getSelectedPage(app);
// 		const todoList: ITodoList = {
// 			id: 0,
// 			items: [],
// 			pageID: selectedPage.id,
// 			title: "Список покупок",
// 			purchasesAreaId: area.id,
// 		};
// 		addPurchasesList(todoList, dispatch);
// 	};

// 	if (loading) return <Loader />;

// 	return (
// 		<>
// 			<h1 className="area-header">{area.header}</h1>
// 			{renderLists(area.purchasesLists)}
// 			<Row>
// 				<AddListBtn onClick={addList} />
// 			</Row>
// 		</>
// 	);
// };

// const PurchasesArea = () => {
// 	return (
// 		<PurchasesAreaState>
// 			<PurchasesAreaLists />
// 		</PurchasesAreaState>
// 	);
// };

// export default PurchasesArea;

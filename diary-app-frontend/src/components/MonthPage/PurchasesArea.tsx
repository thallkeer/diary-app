import React, { useReducer, useContext } from "react";
import { ITodoList, IPurchasesArea } from "../../models";
import { Row, Col } from "react-bootstrap";
import { TodoList } from "../Lists/TodoList";
import { PageAreaResult } from "../../hooks/usePageArea";
import { AddListBtn } from "../AddListBtn";
import { getRandomId } from "../../utils";
import { purchasesAreaReducer } from "../../context/reducers/purchasesArea";
import {
  PurchasesAreaThunks,
  Thunks as thunks,
} from "../../context/actions/purchasesArea-actions";
import { TodoListState } from "../Lists/TodoListState";
import { MonthArea } from "./MonthAreaHOC";

type ListPair = {
  list1: ITodoList;
  list2: ITodoList;
};

const OneList: React.FC<{ todoList: ITodoList }> = ({ todoList }) => {
  const { deletePurchaseList } = useContext(PurchasesAreaState);

  return (
    <Col md={5}>
      <TodoListState
        initList={todoList}
        deleteListFunc={deletePurchaseList}
        isDeletable={true}
      >
        <TodoList className="mt-20 month-lists-header" />
      </TodoListState>
    </Col>
  );
};

export interface IPurchasesAreaState {
  deletePurchaseList: (purchaseList: ITodoList) => void;
}

const PurchasesAreaState = React.createContext<IPurchasesAreaState>(null);

const PurchasesAreaLists: React.FC<PageAreaResult<IPurchasesArea>> = ({
  pageAreaState,
  page,
}) => {
  const [areaState, _dispatch] = useReducer(
    purchasesAreaReducer,
    pageAreaState.area
  );
  const dispatch = (action: PurchasesAreaThunks) => action(_dispatch);

  const deletePurchaseList = (purchaseList: ITodoList) => {
    dispatch(thunks.deletePurchasesList(purchaseList));
  };

  const addPurchaseList = () => {
    const todoList: ITodoList = {
      id: 0,
      items: [],
      pageID: page.id,
      title: "Список покупок",
      purchasesAreaId: areaState.id,
    };

    dispatch(thunks.addPurchasesList(todoList));
  };

  const getRow = (pair: ListPair) => {
    return (
      <Row key={pair.list1.id}>
        {<OneList todoList={pair.list1} />}
        <Col md={2}>{""}</Col>
        {pair.list2 && <OneList todoList={pair.list2} />}
      </Row>
    );
  };

  const renderLists = (todoLists: ITodoList[]) => {
    const rows = [];
    for (let i = 0; i < todoLists.length - 1; i += 2) {
      rows.push(getRow({ list1: todoLists[i], list2: todoLists[i + 1] }));
    }

    if (todoLists.length % 2 !== 0) {
      rows.push(
        getRow({ list1: todoLists[todoLists.length - 1], list2: null })
      );
    }

    return rows;
  };

  return (
    <>
      <PurchasesAreaState.Provider value={{ deletePurchaseList }}>
        {renderLists(areaState.purchasesLists)}
      </PurchasesAreaState.Provider>
      <Row key={areaState.id}>
        <AddListBtn onClick={addPurchaseList} />
      </Row>
    </>
  );
};

const PurchasesArea: React.FC = () => {
  return (
    <MonthArea
      areaName="purchasesArea"
      areaBody={(areaProps: PageAreaResult<IPurchasesArea>) => (
        <PurchasesAreaLists
          pageAreaState={areaProps.pageAreaState}
          page={areaProps.page}
        />
      )}
    />
  );
};

export default PurchasesArea;

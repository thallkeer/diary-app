import React from "react";
import { ITodoList, IPurchasesArea } from "../../models";
import { Row, Col } from "react-bootstrap";
import { TodoList } from "../Lists/TodoList";
import Loader from "../Loader";
import usePageArea from "../../hooks/usePageArea";
import { useTodos } from "../../hooks/useLists";
import { AddListBtn } from "../AddListBtn";
import { getRandomId } from "../../utils";
import axios from "../../axios/axios";

type ListPair = {
  list1: ITodoList;
  list2: ITodoList;
};

export const PurchasesArea: React.FC = () => {
  const { areaState, setAreaState, page } = usePageArea<IPurchasesArea>(
    "purchasesArea"
  );

  const addPurchaseList = () => {
    const todoList: ITodoList = {
      id: 0,
      items: [],
      pageId: page.id,
      title: "Список покупок",
      purchasesAreaId: areaState.area.id
    };

    axios.post("todo", todoList).then(res => {
      todoList.id = res.data;
      setAreaState({
        ...areaState,
        area: {
          ...areaState.area,
          purchasesLists: [...areaState.area.purchasesLists, todoList]
        }
      });
    });
  };

  const OneList: React.FC<{ todoList: ITodoList }> = ({ todoList }) => {
    const { dispatch, list } = useTodos(null, todoList);

    return (
      <Col md={5}>
        <TodoList
          fillToNumber={4}
          className="mt-20 month-lists-header"
          dispatch={dispatch}
          todoList={list}
        />
      </Col>
    );
  };

  const getRow = (pair: ListPair) => {
    return (
      <Row key={pair.list1.id}>
        {<OneList todoList={pair.list1} />}
        <Col md={2}>{""}</Col>
        {pair.list2 && <OneList todoList={pair.list2} />}
        {/* {pair.list2 ? (
          <OneList todoList={pair.list2} />
        ) : (
          <AddListBtn onClick={addPurchaseList} />
        )} */}
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

    rows.push(
      <Row key={getRandomId()}>
        <AddListBtn onClick={addPurchaseList} />
      </Row>
    );

    return rows;
  };

  if (!areaState || areaState.loading) return <Loader />;

  return (
    <>
      <h1>{areaState.area.header}</h1>
      {renderLists(areaState.area.purchasesLists)}
    </>
  );
};

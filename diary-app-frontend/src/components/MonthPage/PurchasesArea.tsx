import React, { useContext } from "react";
import { ITodoList, IPurchasesArea } from "../../models";
import { Row, Col } from "react-bootstrap";
import { TodoList } from "../Lists/TodoList";
import { MonthPageContext } from "../../context";
import Loader from "../Loader";
import { useTodos } from "../../hooks/useLists";
import usePageArea from "../../hooks/usePageArea";

type ListPair = {
  list1: ITodoList;
  list2: ITodoList;
};

export const PurchasesArea: React.FC = () => {
  const [_, dispatch] = useTodos(null); //just for get a dispatch
  const { areaState } = usePageArea<IPurchasesArea>("purchasesArea");

  function getListColumn(list: ITodoList) {
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
  }

  const getRow = (pair: ListPair) => {
    return (
      <Row key={pair.list1.id}>
        {getListColumn(pair.list1)}
        <Col md={2}>{""}</Col>
        {getListColumn(pair.list2)}
      </Row>
    );
  };

  const renderLists = (todoLists: ITodoList[]) => {
    const rows = [];
    for (let i = 0; i < todoLists.length - 1; i += 2) {
      rows.push(getRow({ list1: todoLists[i], list2: todoLists[i + 1] }));
    }

    if (todoLists.length % 2 !== 0)
      rows.push(
        getRow({ list1: todoLists[todoLists.length - 1], list2: null })
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

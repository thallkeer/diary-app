import React, { useEffect, useContext, useState } from "react";
import { ITodoList } from "../../models";
import { Row, Col } from "react-bootstrap";
import { TodoList } from "../Lists/TodoList";
import axios from "axios";
import { MonthPageContext } from "../../context";

type ListPair = {
  list1: ITodoList;
  list2: ITodoList;
};

export const PurchasesArea: React.FC = () => {
  const { page } = useContext(MonthPageContext);
  const [state, setState] = useState<ITodoList[]>([]);

  useEffect(() => {
    page &&
      axios
        .get(`https://localhost:44320/api/events/all/${page.id}`)
        .then(res => res.data);
  }, [page]);

  function getListColumn(list: ITodoList) {
    return (
      <Col md={5}>
        <TodoList fillToNumber={4} className="mt-20 month-lists-header" />
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

  return (
    <>
      <h1>{purchasesArea.header}</h1>
      {renderLists(purchasesArea.purchasesLists)}
    </>
  );
};

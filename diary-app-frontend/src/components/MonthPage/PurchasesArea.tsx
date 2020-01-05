import React, { useContext, useEffect } from "react";
import { IPurchasesArea, ITodoList } from "../../models";
import { Row, Col } from "react-bootstrap";
import { TodoList } from "../Lists/TodoList";
import { GlobalContext } from "../../context";

interface IProps {
  purchasesArea: IPurchasesArea;
}

interface IListPair {
  list1: ITodoList;
  list2: ITodoList;
}

function getListColumn(list: ITodoList) {
  return (
    <Col md={5}>
      <TodoList
        todoList={null}
        fillToNumber={4}
        className="mt-20 month-lists-header"
      />
    </Col>
  );
}

export const PurchasesArea: React.FC<IProps> = ({ purchasesArea }) => {
  const getRow = (pair: IListPair) => {
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

  // const { pageID } = useContext(PageContext);
  // const { month, user, year } = useContext(GlobalContext);

  return (
    <>
      <h1>{purchasesArea.header}</h1>
      {renderLists(purchasesArea.purchasesLists)}
    </>
  );
};

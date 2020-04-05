import React, { useContext, useState, useEffect } from "react";
import { TodoList } from "../Lists/TodoList";
import { MainPageContext } from "../../context";
import { TodoListState } from "../Lists/TodoListState";
import { IMainPage } from "../../models";
import Loader from "../Loader";

export const ImportantThings = () => {
  const pageState = useContext(MainPageContext);

  const [page, setPage] = useState<IMainPage>();

  useEffect(() => {
    const check: boolean = pageState && pageState.page !== null;
    if (check) setPage(pageState.page);
  }, [pageState.page]);

  if (!page) return <Loader />;

  return (
    <TodoListState page={page}>
      <TodoList />
    </TodoListState>
  );
};

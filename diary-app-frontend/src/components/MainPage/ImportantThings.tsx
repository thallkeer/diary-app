import React, { useContext } from "react";
import { TodoList } from "../Lists/TodoList";
import { MainPageContext } from "../../context";
import Loader from "../Loader";
import { useTodos } from "../../hooks/useLists";

export const ImportantThings = () => {
  const { page } = useContext(MainPageContext);
  const { loading, list, dispatch } = useTodos(page);

  if (loading || !list)
    return (
      <div>
        <Loader />
      </div>
    );

  return <TodoList fillToNumber={6} todoList={list} dispatch={dispatch} />;
};

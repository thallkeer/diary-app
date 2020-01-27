import React, { useContext } from "react";
import { TodoList } from "../Lists/TodoList";
import { MainPageContext } from "../../context";
import Loader from "../Loader";
import { useTodos } from "../../hooks/useLists";

export const ImportantThings = () => {
  const { page } = useContext(MainPageContext);
  const state = useTodos(page);

  if (state.loading || !state.list) return <Loader />;

  return (
    <TodoList
      fillToNumber={6}
      todoList={state.list}
      dispatch={state.dispatch}
    />
  );
};

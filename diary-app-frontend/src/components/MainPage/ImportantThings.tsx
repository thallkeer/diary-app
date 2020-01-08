import React, { useReducer, useContext, useEffect } from "react";
import { TodoList } from "../Lists/TodoList";
import { MainPageContext, TodoListContext } from "../../context";
import { TodoThunks, Thunks as todoThunks } from "../../actions/todo-actions";
import { todosReducer } from "../../context/todos";
import Loader from "../Loader";

export const ImportantThings = () => {
  const [state, _dispatch] = useReducer(todosReducer, {
    list: null,
    loading: false,
    dispatch: () => {}
  });
  const dispatch = (action: TodoThunks) => action(_dispatch);
  const { page } = useContext(MainPageContext);

  useEffect(() => {
    dispatch(todoThunks.loadTodos(page.id));
  }, [page]);

  if (state.loading || !state.list) return <Loader />;

  return (
    <TodoListContext.Provider value={{ ...state, dispatch }}>
      <TodoList fillToNumber={6} />
    </TodoListContext.Provider>
  );
};

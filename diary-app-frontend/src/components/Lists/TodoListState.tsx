import React, { useReducer, useEffect } from "react";
import {
  TodoThunks,
  Thunks as todoThunks
} from "../../context/actions/todo-actions";
import { IPage, ITodoList, ITodo } from "../../models";
import { TodoListContext } from "../../context";
import { todosReducer } from "../../context/todos";

export const TodoListState: React.FC<{
  page?: IPage;
  initList?: ITodoList;
}> = ({ page, initList, children }) => {
  const [state, _dispatch] = useReducer(todosReducer, {
    list: initList || null,
    loading: false
  });

  const dispatch = (action: TodoThunks) => action(_dispatch);

  const { list } = state;

  const {
    toggleTodo,
    deleteTodo,
    addOrUpdateTodo,
    updateTodoList,
    loadTodosByPageID,
    setTodoList
  } = todoThunks;

  console.log("render todolist", list);

  useEffect(() => {
    if (list !== null) return;

    if (initList) {
      console.log("todolist effect setTodoList", list);
      dispatch(setTodoList(initList));
    } else if (page && !list) {
      console.log("todolist effect loadTodoList", list);
      dispatch(loadTodosByPageID(page.id));
    }
  }, [page, initList, list]);

  const toggleTodoItem = (todoId: number) => {
    todoId !== 0 && dispatch(toggleTodo(todoId));
  };

  const deleteTodoItem = (todoId: number) => {
    todoId !== 0 && dispatch(deleteTodo(todoId));
  };

  const updateTodo = (todo: ITodo) => {
    dispatch(
      addOrUpdateTodo({
        ...todo,
        ownerID: list.id
      })
    );
  };

  const updateListTitle = (title: string) => {
    dispatch(
      updateTodoList({
        ...list,
        title
      })
    );
  };

  return (
    <TodoListContext.Provider
      value={{
        ...state,
        updateListTitle,
        deleteItem: deleteTodoItem,
        toggleTodoItem,
        addOrUpdateItem: updateTodo
      }}
    >
      {children}
    </TodoListContext.Provider>
  );
};

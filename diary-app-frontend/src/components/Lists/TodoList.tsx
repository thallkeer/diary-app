import React, { useEffect, useContext } from "react";
import { TodoInput } from "./TodoInput";
import "./style.css";
import { Thunks as todoThunks } from "../../actions/todo-actions";
import { Store } from "../../context";
import Loader from "../Loader";
import { getEmptyTodo } from "../../utils";

interface IProps {
  header: string;
}

export const TodoList: React.FC<IProps> = ({ header }) => {
  const { list, loading, dispatch } = useContext(Store).todos;

  useEffect(() => {
    dispatch && dispatch(todoThunks.loadTodos(header));
  }, []);

  const toggleTodo = (todoId: number) => {
    if (todoId !== 0) dispatch(todoThunks.toggleTodo(todoId));
  };

  const updateTodo = (todoId: number, todoText: string) => {
    dispatch(
      todoThunks.addOrUpdateTodo({
        id: todoId,
        subject: todoText,
        done: list.items.find(t => t.id === todoId).done,
        ownerID: list.id
      })
    );
  };

  const todos = list.items.length === 0 ? [getEmptyTodo()] : list.items;

  return (
    <div style={{ marginTop: "52px" }}>
      <h1 className="todo-list-header">{header}</h1>
      {loading ? (
        <Loader />
      ) : (
        <ul className="todos">
          {todos.map(todo => (
            <li key={todo.id}>
              <TodoInput
                updateItem={updateTodo}
                todo={todo}
                toggleTodo={toggleTodo}
              />
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

import React, { useContext } from "react";
import { TodoInput } from "./TodoInput";
import { Thunks as todoThunks } from "../../actions/todo-actions";
import { Store } from "../../context";
import Loader from "../Loader";
import { getEmptyTodo } from "../../utils";
import { ITodoList } from "../../models";

interface IProps {
  todoList: ITodoList;
  className?: string;
  fillToNumber?: number; //number of items list should contains (if it's not contains required number of items, they will be added as empty items)
  loading?: boolean;
}

export const TodoList: React.FC<IProps> = ({
  todoList,
  fillToNumber,
  className,
  loading = false
}) => {
  const { dispatch } = useContext(Store).todos;

  const toggleTodo = (todoId: number) => {
    if (todoId !== 0) dispatch(todoThunks.toggleTodo(todoId));
  };

  const updateTodo = (todoId: number, todoText: string) => {
    dispatch(
      todoThunks.addOrUpdateTodo({
        id: todoId,
        subject: todoText,
        done: todoList.items.find(t => t.id === todoId).done,
        ownerID: todoList.id
      })
    );
  };

  const todos = [...todoList.items];

  if (fillToNumber && todos.length < fillToNumber) {
    for (let i = todos.length; i < fillToNumber; i++) {
      todos.push(getEmptyTodo());
    }
  }

  return (
    <div style={{ marginTop: "52px" }} className={className}>
      {loading ? (
        <Loader />
      ) : (
        <>
          <h1 className="todo-list-header">{todoList.title}</h1>
          <ul className="todos">
            {todos.map((todo, i) => (
              <li key={todo.id !== 0 ? todo.id : i * 80}>
                <TodoInput
                  updateItem={updateTodo}
                  todo={todo}
                  toggleTodo={toggleTodo}
                />
              </li>
            ))}
          </ul>
        </>
      )}
    </div>
  );
};

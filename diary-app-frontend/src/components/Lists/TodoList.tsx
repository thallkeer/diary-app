import React, { useContext } from "react";
import { TodoInput } from "./TodoInput";
import { Thunks as todoThunks } from "../../actions/todo-actions";
import { TodoListContext } from "../../context";
import Loader from "../Loader";
import { getEmptyTodo } from "../../utils";
import { ITodo } from "../../models";
import { useFillToNumber } from "../../hooks/useFillToNumber";

interface IProps {
  className?: string;
  fillToNumber?: number; //number of items list should contains (if it's not contains required number of items, they will be added as empty items)
}

export const TodoList: React.FC<IProps> = ({ fillToNumber = 0, className }) => {
  const { list, dispatch, loading } = useContext(TodoListContext);
  const { addOrUpdateTodo, toggleTodo } = todoThunks;

  const todos: ITodo[] = useFillToNumber(
    [...list.items],
    fillToNumber,
    getEmptyTodo
  );

  const toggleTodoItem = (todoId: number) => {
    todoId !== 0 && dispatch(toggleTodo(todoId));
  };

  const updateTodo = (todo: ITodo) => {
    dispatch(
      addOrUpdateTodo({
        ...todo,
        ownerID: list.id
      })
    );
  };

  if (loading) return <Loader />;

  return (
    <div className={`mt-52 ${className}`}>
      <h1 className="todo-list-header">{list.title}</h1>
      <ul className="todos">
        {todos.map((todo, i) => (
          <li key={todo.id !== 0 ? todo.id : i * 80}>
            <TodoInput
              updateItem={updateTodo}
              todo={todo}
              toggleTodo={toggleTodoItem}
            />
          </li>
        ))}
      </ul>
    </div>
  );
};

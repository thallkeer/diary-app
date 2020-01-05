import React, { useContext, useReducer } from "react";
import { TodoInput } from "./TodoInput";
import { Thunks as todoThunks } from "../../actions/todo-actions";
import { ListState } from "../../context";
import Loader from "../Loader";
import { getEmptyTodo } from "../../utils";
import { ITodoList, ITodo } from "../../models";
import { useFillToNumber } from "../../hooks/useFillToNumber";
import { todosReducer } from "../../context/todos";

interface IProps {
  todoList: ListState<ITodo>;
  className?: string;
  fillToNumber?: number; //number of items list should contains (if it's not contains required number of items, they will be added as empty items)
  loading?: boolean;
}

export const TodoList: React.FC<IProps> = ({
  todoList,
  fillToNumber = 0,
  className
}) => {
  const [{ list, loading }, dispatch] = useReducer(todosReducer, todoList);

  const todos: ITodo[] = useFillToNumber(
    list.items,
    fillToNumber,
    getEmptyTodo
  );

  const toggleTodo = (todoId: number) => {
    if (todoId !== 0) todoThunks.toggleTodo(todoId)(dispatch);
  };

  const updateTodo = (todoId: number, todoText: string) => {
    todoThunks.addOrUpdateTodo({
      id: todoId,
      subject: todoText,
      done: todos.find(t => t.id === todoId).done,
      ownerID: list.id
    })(dispatch);
  };

  if (loading) return <Loader />;

  return (
    <div /*mt-52 */ className={className}>
      <h1 className="todo-list-header">{list.title}</h1>
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
    </div>
  );
};

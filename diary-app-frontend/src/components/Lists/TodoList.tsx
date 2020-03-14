import React, { useContext } from "react";
import { TodoInput } from "./TodoInput";
import { DeleteBtn } from "./DeleteBtn";
import ListHeaderInput from "./ListHeaderInput";
import { FaLink } from "react-icons/fa";
import { TodoListContext } from "../../context";
import Loader from "../Loader";
import { getEmptyTodo, fillToNumber } from "../../utils";

type TodoListProps = {
  className?: string;
};

export const TodoList: React.FC<TodoListProps> = ({ className }) => {
  const {
    list,
    loading,
    deleteItem,
    toggleTodoItem,
    updateListTitle,
    addOrUpdateItem
  } = useContext(TodoListContext);

  if (loading || !list) return <Loader />;

  const todos = fillToNumber([...list.items], 6, getEmptyTodo);

  return (
    <div className={`mt-52 ${className}`}>
      <h1 className="todo-list-header">
        <ListHeaderInput handleBlur={updateListTitle} value={list.title} />
      </h1>
      <ul className="todos">
        {todos.map((todo, i) => (
          <li key={todo.id !== 0 ? todo.id : i * -80} className="list-item">
            <TodoInput
              updateItem={() => addOrUpdateItem(todo)}
              todo={todo}
              toggleTodo={() => toggleTodoItem(todo.id)}
            />
            {todo.id !== 0 && (
              <>
                {todo.url && (
                  <a
                    className="list-item-link"
                    href={todo.url}
                    target="_blank"
                    rel="noopener noreferrer"
                  >
                    <FaLink style={{ color: "lightblue" }}></FaLink>
                  </a>
                )}
                <DeleteBtn onDelete={() => deleteItem(todo.id)} />
              </>
            )}
          </li>
        ))}
      </ul>
    </div>
  );
};

import React, { useContext } from "react";
import { TodoInput } from "./TodoInput";
import ListHeaderInput from "./ListHeaderInput";
import { TodoListContext } from "../../context";
import Loader from "../Loader";
import { getEmptyTodo, fillToNumber } from "../../utils";
import { DeleteBtn } from "./DeleteBtn";

export const TodoList: React.FC<React.HtmlHTMLAttributes<
  HTMLDivElement
>> = () => {
  const {
    list,
    loading,
    deleteItem,
    toggleTodoItem,
    updateListTitle,
    addOrUpdateItem,
    deleteTodoList,
    isDeletable,
  } = useContext(TodoListContext);

  if (loading || !list) return <Loader />;

  const todos = fillToNumber([...list.items], 6, getEmptyTodo);

  return (
    <div className={`mt-52`}>
      <h1 className="todo-list-header">
        <ListHeaderInput handleBlur={updateListTitle} value={list.title} />
        {isDeletable && <DeleteBtn onDelete={() => deleteTodoList(list)} />}
      </h1>
      <ul className="todos">
        {todos.map((todo, i) => (
          <li key={todo.id !== 0 ? todo.id : i * -80} className="list-item">
            <TodoInput
              todo={todo}
              updateTodo={addOrUpdateItem}
              toggleTodo={toggleTodoItem}
              deleteTodo={deleteItem}
            />
          </li>
        ))}
      </ul>
    </div>
  );
};

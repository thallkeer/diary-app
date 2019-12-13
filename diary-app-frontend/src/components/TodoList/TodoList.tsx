import React, { useEffect, useReducer } from "react";
import { TodoInput } from "./TodoInput";
import "./style.css";
import { todosReducer } from "../../contexts/todos";
import { Thunks as todoThunks } from "../../actions/todo-actions";

interface IProps {
  header: string;
}

export const TodoList: React.FC<IProps> = ({ header }) => {
  const [{ loading, todos, thunks }, _dispatch] = useReducer(todosReducer, {
    todos: [],
    loading: false,
    thunks: todoThunks
  });

  const dispatch = action => action(_dispatch);

  useEffect(() => {
    dispatch(thunks.loadTodos());
  }, []);

  const toggleTodo = (todoId: number) => {
    dispatch(thunks.toggleTodo(todoId));
  };

  const updateTodo = (todoId: number, todoText: string) => {
    console.log(todoId, todoText);
    dispatch(thunks.addOrUpdateTodo(todoId, todoText));
  };

  return (
    <div style={{ marginTop: "52px" }}>
      <h1 className="todo-list-header">{header}</h1>
      {loading ? (
        <div>
          <h2>Loading...</h2>
        </div>
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

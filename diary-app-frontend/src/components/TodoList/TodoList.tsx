import React, { useState, useEffect } from "react";
import { TodoInput } from "./TodoInput";
import { getTodos } from "../../services/todoService";
import "./style.css";
import { TodoListContext } from "../../contexts";
import { getEmptyTodo } from "../../utils";
import { ITodoItem } from "../../models/index";

interface IProps {
  header: string;
}

export const TodoList: React.FC<IProps> = ({ header }) => {
  const [todos, setTodos] = useState<ITodoItem[] | null>([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    setLoading(true);
    getTodos().then(res => {
      setTodos([...res, getEmptyTodo()]);
      setLoading(false);
    });
  }, []);

  const toggleTodo = (todoId: number) => {
    setTodos(
      todos.map(todo =>
        todo.id === todoId ? { ...todo, done: !todo.done } : todo
      )
    );
  };

  const updateTodo = (todoId: number, todoText: string) => {
    const newTodos = [
      ...todos.map(t => (t.id === todoId ? { ...t, text: todoText } : t)),
      getEmptyTodo()
    ];

    setTodos(newTodos);
  };

  return (
    <div style={{ marginTop: "52px" }}>
      <h1 className="todo-list-header">{header}</h1>
      {loading ? (
        <div>
          <h2>Loading...</h2>
        </div>
      ) : (
        <ul className="todo-list-list">
          {todos.map(todo => (
            <li
              key={todo.id}
              style={{
                display: "flex",
                justifyContent: "space-between",
                alignItems: "flex-start",
                padding: ".5rem 1rem",
                marginBottom: ".5rem"
              }}
            >
              <TodoListContext.Provider
                value={{ todo: todo, toggleTodo: toggleTodo }}
              >
                <TodoInput updateItem={updateTodo} />
              </TodoListContext.Provider>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

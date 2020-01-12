import React from "react";
import { TodoInput } from "./TodoInput";
import { Thunks as todoThunks, TodoThunks } from "../../actions/todo-actions";
import { getEmptyTodo } from "../../utils";
import { ITodo, ITodoList } from "../../models";
import { useFillToNumber } from "../../hooks/useFillToNumber";
import { DeleteBtn } from "./DeleteBtn";

type TodoListProps = {
  className?: string;
  fillToNumber?: number; //number of items list should contains (if it's not contains required number of items, they will be added as empty items)
  todoList: ITodoList;
  dispatch: (action: TodoThunks) => void;
};

export const TodoList: React.FC<TodoListProps> = ({
  fillToNumber = 0,
  className,
  todoList,
  dispatch
}) => {
  const { addOrUpdateTodo, toggleTodo, deleteTodo } = todoThunks;

  console.log("todo list component ", todoList);

  const todos: ITodo[] = useFillToNumber(
    [...todoList.items],
    fillToNumber,
    getEmptyTodo
  );

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
        ownerID: todoList.id
      })
    );
  };

  return (
    <div className={`mt-52 ${className}`}>
      <h1 className="todo-list-header">{todoList.title}</h1>
      <ul className="todos">
        {todos.map((todo, i) => (
          <li key={todo.id !== 0 ? todo.id : i * 80} className="list-item">
            <TodoInput
              updateItem={updateTodo}
              todo={todo}
              toggleTodo={toggleTodoItem}
            />
            {todo.id !== 0 && (
              <DeleteBtn onDelete={() => deleteTodoItem(todo.id)} />
            )}
          </li>
        ))}
      </ul>
    </div>
  );
};

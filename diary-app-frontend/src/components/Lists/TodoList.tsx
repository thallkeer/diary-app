import React from "react";
import { TodoInput } from "./TodoInput";
import { Thunks as todoThunks, TodoThunks } from "../../actions/todo-actions";
import { ITodo, ITodoList } from "../../models";
import { DeleteBtn } from "./DeleteBtn";
import ListHeaderInput from "./ListHeaderInput";
import { useTodoList } from "../../hooks/useLists";

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
  const {
    addOrUpdateTodo,
    toggleTodo,
    deleteTodo,
    updateTodoList
  } = todoThunks;

  const { title, setTitle, todos } = useTodoList(todoList, fillToNumber);

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

  const handleBlur = () => {
    dispatch(
      updateTodoList({
        ...todoList,
        title
      })
    );
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) =>
    setTitle(e.target.value);

  return (
    <div className={`mt-52 ${className}`}>
      <h1 className="todo-list-header">
        <ListHeaderInput
          handleChange={handleChange}
          handleBlur={handleBlur}
          value={title}
        />
      </h1>
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

import React, { FC, useState, useEffect } from "react";
import { CheckCircle } from "./CheckCircle";
import { ListItemInput, UrlInput } from "./ListItemInput";
import { ITodo } from "../../models";
import { ContextMenu, MenuItem, ContextMenuTrigger } from "react-contextmenu";

interface ITodoInputProps {
  todo: ITodo;
  toggleTodo: (todoId: number) => void;
  updateTodo: (todo: ITodo) => void;
  deleteTodo: (todoID: number) => void;
}

export const TodoInput: FC<ITodoInputProps> = ({
  updateTodo,
  toggleTodo,
  deleteTodo,
  todo,
}) => {
  const [editUrlMode, setEditUrlMode] = useState(false);

  const handleAddClick = () => {
    setEditUrlMode(true);
  };

  const handleDeleteClick = () => {
    console.log("handle delete click", todo);
    deleteTodo(todo.id);
  };

  const updateTodoItem = (todoItem: ITodo) => {
    updateTodo(todoItem);
    setEditUrlMode(false);
  };

  useEffect(() => {}, [todo, updateTodo, deleteTodo, toggleTodo, editUrlMode]);

  const todoInput = (
    <>
      <CheckCircle id={todo.id} done={todo.done} toggleTodo={toggleTodo} />
      {editUrlMode ? (
        <UrlInput
          item={todo}
          updateItem={updateTodoItem}
          endEdit={() => setEditUrlMode(false)}
        />
      ) : (
        <ListItemInput
          item={todo}
          updateItem={updateTodoItem}
          readonly={todo.readonly}
        />
      )}
    </>
  );

  if (todo.id === 0) return todoInput;

  return (
    <>
      <ContextMenuTrigger id={`context-menu-${todo.id}`}>
        {todoInput}
      </ContextMenuTrigger>
      <ContextMenu className="menu" id={`context-menu-${todo.id}`}>
        <MenuItem onClick={handleAddClick} className="menuItem">
          Добавить/Изменить гиперссылку
        </MenuItem>
        <MenuItem onClick={handleDeleteClick} className="menuItem">
          Удалить запись
        </MenuItem>
      </ContextMenu>
    </>
  );
};

import React, { FC } from "react";
import { CheckCircle } from "./CheckCircle";
import { ListItemInput } from "./ListItemInput";
import { ITodo } from "../../models";

interface ITodoInputProps {
  todo: ITodo;
  toggleTodo: (todoId: number) => void;
  updateItem: (todo: ITodo) => void;
}

export const TodoInput: FC<ITodoInputProps> = ({
  updateItem,
  toggleTodo,
  todo
}) => {
  return (
    <>
      <CheckCircle id={todo.id} done={todo.done} toggleTodo={toggleTodo} />
      <ListItemInput item={todo} updateItem={updateItem} canEditUrl={true} />
    </>
  );
};

import React, { FC } from "react";
import { CheckCircle } from "./CheckCircle";
import { ListItemInput } from "./ListItemInput";
import { ITodoItem } from "../../models";

interface ITodoInputProps {
  todo: ITodoItem;
  toggleTodo: (todoId: number) => void;
  updateItem: (id: number, text: string) => void;
}

export const TodoInput: FC<ITodoInputProps> = ({
  updateItem,
  toggleTodo,
  todo
}) => {
  return (
    <>
      <CheckCircle id={todo.id} done={todo.done} toggleTodo={toggleTodo} />
      {/* {todo.id !== 0 && (
        <CheckCircle id={todo.id} done={todo.done} toggleTodo={toggleTodo} />
      )} */}
      <ListItemInput
        itemId={todo.id}
        itemText={todo.subject}
        updateItem={updateItem}
      />
    </>
  );
};

import React, { FC, useContext } from "react";
import { CheckCircle } from "./CheckCircle";
import { ListItemInput } from "../ListItemInput";
import { TodoListContext } from "../../contexts";

interface ITodoInputProps {
  updateItem: (id: number, text: string) => void;
}

export const TodoInput: FC<ITodoInputProps> = ({ updateItem }) => {
  const { todo, toggleTodo } = useContext(TodoListContext);

  return (
    <>
      <CheckCircle id={todo.id} done={todo.done} toggleTodo={toggleTodo} />
      <ListItemInput
        itemId={todo.id}
        itemText={todo.text}
        updateItem={updateItem}
      />
    </>
  );
};

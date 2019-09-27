import React, { FunctionComponent } from "react";
import ConnectedCheckCircle from "./CheckCircle";
import ITodoItem from "../../models/todo-model";
import ListItemInput from "../ListItemInput";

const TodoInput: FunctionComponent<ITodoItem> = (todo: ITodoItem) => {
  return (
    <div style={{ marginTop: "10px" }}>
      <ConnectedCheckCircle id={todo.id} done={todo.done} />
      <ListItemInput itemId={todo.id} itemText={todo.text} />
    </div>
  );
};

export default TodoInput;

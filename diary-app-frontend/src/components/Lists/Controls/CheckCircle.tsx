import React from "react";

interface IProps {
  id: number;
  done: boolean;
  toggleTodo: (todoId: number) => void;
}

export const CheckCircle: React.FC<IProps> = ({ id, done, toggleTodo }) => {
  return (
    <span
      onClick={() => toggleTodo(id)}
      className="check-circle"
      style={{ backgroundColor: done ? "lightblue" : "white" }}
    />
  );
};

import { ITodoItem } from "../models/index";

var _getRandomInt = (min: number, max: number) => {
  return Math.floor(Math.random() * (max - min + 1)) + min;
};

export const getRandomId = () => {
  var ts = new Date().getTime().toString();
  var parts = ts.split("").reverse();
  var id = "";

  for (var i = 0; i < 8; ++i) {
    var index = _getRandomInt(0, parts.length - 1);
    id += parts[index];
  }
  return Number(id);
};

export const getEmptyTodo = () => {
  const todo: ITodoItem = { id: 0, text: "", done: false };
  return todo;
};

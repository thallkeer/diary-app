import axios from "../axios/axios";
import { ITodo, ITodoList } from "../models";
import { getListService } from "./listService";

const listService = getListService<ITodoList, ITodo>("todo/", "Todo");

const toggleTodo = async (todoId: number) => {
	await axios.put(`todo/toggle/${todoId}`);
};

export const todosService = {
	...listService,
	toggleTodo,
};

import axios from "../axios/axios";
import { ITodo, ITodoList } from "../models";
import { getListService } from "./listService";

const listService = getListService<ITodoList, ITodo>("todo/");

const toggleTodo = async (todoId: number) => {
	return axios.put(`todo/items/toggle/${todoId}`);
};

export const todosService = {
	...listService,
	toggleTodo,
};

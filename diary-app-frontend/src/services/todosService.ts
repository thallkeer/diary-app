import { ITodo, ITodoList } from "models";
import { ListItemUrls, ListUrls } from "../models/types";
import { CrudService } from "./crudService";

class TodoItemService extends CrudService<ITodo> {
	constructor() {
		const url: ListItemUrls = "todos";
		super(url);
	}

	toggleTodo(todoId: number) {
		if (todoId === 0) throw Error("todoId cannot be zero");
		return this.axios
			.put(`${this.apiUrl}/toggle/${todoId}`)
			.then((res) => res.data);
	}
}

const listUrl: ListUrls = "todoLists";
const todoService = new TodoItemService();
const todoListService = new CrudService<ITodoList>(listUrl);

export const todosService = {
	list: todoListService,
	items: todoService,
};

import {
	ICommonList,
	IEvent,
	IEventList,
	IListItem,
	ITodo,
	ITodoList,
} from "models";
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

const todoListUrl: ListUrls = "todoLists";
const todoService = new TodoItemService();
const todoListService = new CrudService<ITodoList>(todoListUrl);

const eventListUrl: ListUrls = "eventLists";
const eventItemUrl: ListItemUrls = "events";
export const eventListService = new CrudService<IEventList>(eventListUrl);
export const eventItemService = new CrudService<IEvent>(eventItemUrl);

const commonListUrl: ListUrls = "commonLists";
const listItemUrl: ListItemUrls = "listItems";
export const commonListService = new CrudService<ICommonList>(commonListUrl);
export const listItemService = new CrudService<IListItem>(listItemUrl);

export const todosService = {
	list: todoListService,
	items: todoService,
};

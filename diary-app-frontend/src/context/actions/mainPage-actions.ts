import { ActionsUnion, createAction } from "./action-helpers";
import { ITodoListState } from "../../components/Lists/TodoList/TodoListState";
import { IEventListContext } from "../../components/Lists/EventList/EventListState";
import { IMainPage } from "../../models";

export const SET_PAGE = "SET_PAGE";
export const SET_EVENTS = "SET_EVENTS";
export const SET_TODOS = "SET_TODOS";

export const Actions = {
	setEvents: (eventListContext: IEventListContext) =>
		createAction(SET_EVENTS, eventListContext),
	setTodos: (todoListContext: ITodoListState) =>
		createAction(SET_TODOS, todoListContext),
	setPage: (mainPage: IMainPage) => createAction(SET_PAGE, mainPage),
};

export type MainPageActions = ActionsUnion<typeof Actions>;

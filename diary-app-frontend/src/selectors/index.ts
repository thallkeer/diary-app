import { createSelector, ParametricSelector } from "reselect";
import {
	IEvent,
	IListState,
	IListItem,
	ITodo,
	IList,
	IEventList,
	ITodoList,
} from "../models/index";
import { AppState } from "../context/reducers/app-reducer";
import { ITodoListState } from "../context/reducers/list/todos";
import { AppStateType } from "../context/store";
import { IEventListState } from "../context/reducers/list/events";
import { getImportantEventsList } from "./page-selectors";

const getSelectedPageFromState = (state: AppState) =>
	state.selectedPage ? state.selectedPage : null;

export const getSelectedPage = createSelector(
	[getSelectedPageFromState],
	(s) => s
);

const getEventsState = (state: IEventListState) =>
	getListState<IEventListState, IEventList, IEvent>(state);

export const getEvents = createSelector([getEventsState], (s) => s);

export const getEventsByDay = createSelector(
	[getImportantEventsList],
	(importantEvents) => {
		let eventsMap = new Map<number, IEvent[]>();

		const events = importantEvents.list?.items ?? [];

		if (events && events.length) {
			events.forEach((ev) => {
				let day = ev.date.getDate();
				if (!eventsMap.has(day)) eventsMap.set(day, []);
				eventsMap.get(day).push(ev);
			});
		}

		return eventsMap;
	}
);

const getTodosState = (state: ITodoListState) =>
	getListState<ITodoListState, ITodoList, ITodo>(state);

export const getTodos = createSelector([getTodosState], (s) => s);

function getListState<
	T extends IListState<TList, TItem>,
	TList extends IList<TItem>,
	TItem extends IListItem
>(state: T) {
	console.log("returning state of list ", state.list);

	return state.list?.items ?? [];
}

export function createPropSelector<T>(key: keyof T) {
	return function (state: AppStateType, props: T) {
		return props[key];
	} as ParametricSelector<AppStateType, T, T[typeof key]>;
}

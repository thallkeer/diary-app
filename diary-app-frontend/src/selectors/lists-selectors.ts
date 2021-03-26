import { createSelector, ParametricSelector } from "reselect";
import {
	IEvent,
	IListItem,
	ITodo,
	IDiaryList,
	IEventList,
	ITodoList,
} from "models";
import { ITodoListState } from "store/diaryLists";
import { AppStateType } from "store/reducer";
import { IEventListState } from "store/diaryLists";
import { getImportantEventsList } from "../store/pages/pages.selectors";
import { IListState } from "models/states";

const getEventsState = (state: IEventListState) =>
	getListState<IEventListState, IEventList, IEvent>(state);

export const getEvents = createSelector([getEventsState], (s) => s);

export const getEventsByDay = createSelector(
	[getImportantEventsList],
	(importantEvents) => {
		const eventsMap = new Map<number, IEvent[]>();

		const events = importantEvents.list?.items ?? [];

		if (events && events.length) {
			events.forEach((ev) => {
				const day = ev.date.getDate();
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
	TList extends IDiaryList<TItem>,
	TItem extends IListItem
>(state: T) {
	return state.list?.items ?? [];
}

export function createPropSelector<T>(key: keyof T) {
	return function (state: AppStateType, props: T) {
		return props[key];
	} as ParametricSelector<AppStateType, T, T[typeof key]>;
}

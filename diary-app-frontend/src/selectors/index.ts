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
// import { IPurchasesAreaState } from "../context/reducers/pageArea/purchasesArea-reducer";
// import { IGoalsAreaState } from "../context/reducers/pageArea/goalsArea-reducer";
import { AppState } from "../context/reducers/app-reducer";
import { ITodoListState } from "../context/reducers/list/todos";
import { AppStateType } from "../context/store";

// const getGoalsListsFromState = (state: IGoalsAreaState) =>
// 	state.area ? state.area.goalsLists : [];

// export const getGoalsLists = createSelector([getGoalsListsFromState], (s) => s);

// const getPurchasesListsFromState = (state: IPurchasesAreaState) =>
// 	state.area ? state.area.purchasesLists : [];

// export const getPurchasesLists = createSelector(
// 	[getPurchasesListsFromState],
// 	(s) => s
// );

const getSelectedPageFromState = (state: AppState) =>
	state.selectedPage ? state.selectedPage : null;

export const getSelectedPage = createSelector(
	[getSelectedPageFromState],
	(s) => s
);

// const getEventsState = (state: IEventListState) =>
// 	getListState<IEventListState, IEventList, IEvent>(state);

// export const getEvents = createSelector([getEventsState], (s) => s);

// export const getEventsByDay = createSelector([getEventsState], (s) => {
// 	let eventsMap = new Map<number, IEvent[]>();

// 	if (s && s.length) {
// 		s.forEach((ev) => {
// 			let day = ev.date.getDate();
// 			if (!eventsMap.has(day)) eventsMap.set(day, []);
// 			eventsMap.get(day).push(ev);
// 		});
// 	}

// 	return eventsMap;
// });

const getTodosState = (state: ITodoListState) =>
	getListState<ITodoListState, ITodoList, ITodo>(state);

export const getTodos = createSelector([getTodosState], (s) => s);

function getListState<
	T extends IListState<TList, TItem>,
	TList extends IList<TItem>,
	TItem extends IListItem
>(state: T) {
	return state && state.list ? state.list.items : [];
}

export function getListItems<
	T extends IListState<TList, TItem>,
	TList extends IList<TItem>,
	TItem extends IListItem
>() {
	const res = createSelector([getListState<T, TList, TItem>], s => s);
}

export function createPropSelector<T>(key: keyof T) {
	return function (state: AppStateType, props: T) {
		return props[key];
	} as ParametricSelector<AppStateType, T, T[typeof key]>;
}

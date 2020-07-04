import { createSelector } from "reselect";
import {
	IEvent,
	IListState,
	IListItem,
	ITodo,
	IList,
	IEventList,
	ITodoList,
} from "../models/index";
import { ITodoListState } from "../components/Lists/TodoList/TodoListState";
import { IEventListState } from "../components/Lists/EventList/EventListState";
import { IMainPageState } from "../components/MainPage/MainPageState";
import { IAppState } from "../context";
import { IPurchasesAreaState } from "../context/reducers/pageArea/purchasesArea";
import { IGoalsAreaState } from "../context/reducers/pageArea/goalsArea";

const getGoalsListsFromState = (state: IGoalsAreaState) =>
	state.area ? state.area.goalsLists : [];

export const getGoalsLists = createSelector([getGoalsListsFromState], (s) => s);

const getPurchasesListsFromState = (state: IPurchasesAreaState) =>
	state.area ? state.area.purchasesLists : [];

export const getPurchasesLists = createSelector(
	[getPurchasesListsFromState],
	(s) => s
);

const getSelectedPageFromState = (state: IAppState) =>
	state.selectedPage ? state.selectedPage.page : null;

export const getSelectedPage = createSelector(
	[getSelectedPageFromState],
	(s) => s
);

const getPageEventsState = (mainPageState: IMainPageState) =>
	mainPageState.mainPage && mainPageState.mainPage.events
		? mainPageState.mainPage.events.eventListState
		: null;
export const getPageEvents = createSelector([getPageEventsState], (s) => s);

const getEventsState = (state: IEventListState) =>
	getListState<IEventListState, IEventList, IEvent>(state);

export const getEvents = createSelector([getEventsState], (s) => s);

export const getEventsByDay = createSelector([getEventsState], (s) => {
	let eventsMap = new Map<number, IEvent[]>();

	if (s && s.length) {
		s.forEach((ev) => {
			let day = ev.date.getDate();
			if (!eventsMap.has(day)) eventsMap.set(day, []);
			eventsMap.get(day).push(ev);
		});
	}

	return eventsMap;
});

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

export const getListItems = createSelector([getListState], (s) => s);

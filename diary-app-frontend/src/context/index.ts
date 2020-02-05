import { createContext } from "react";
import {
  IList,
  IMainPage,
  ListItem,
  IPage,
  IMonthPage,
  IHabitsTracker,
  IEventList,
  ITodoList
} from "../models/index";
import { EventThunks } from "../actions/events-actions";
import { TodoThunks } from "../actions/todo-actions";
import { HabitTrackerThunks } from "../actions/habitTracker-actions";

export interface IGlobalContext {
  month: number;
  year: number;
  setAppState: (appState: IGlobalContext) => void;
}

export type PageState<T extends IPage> = {
  page: T;
  loading: boolean;
};

export interface IMainPageContext extends PageState<IMainPage> {
  events: IEventListContext;
  setPageState?: (pageState: IMainPageContext) => void;
}

export interface IMonthPageContext extends PageState<IMonthPage> {
  setPageState?: (pageState: IMonthPageContext) => void;
}

interface IDispatchable<TAction> {
  dispatch: (action: TAction) => void;
}

export interface IListState<TList extends IList<ListItem>, TAction>
  extends IDispatchable<TAction> {
  list: TList;
  loading: boolean;
}

export interface IEventListContext
  extends IListState<IEventList, EventThunks> {}
export interface ITodoListContext extends IListState<ITodoList, TodoThunks> {}
export interface IHabitTrackerContext
  extends IDispatchable<HabitTrackerThunks> {
  trackers: IHabitsTracker[];
}

export const EventListContext = createContext<IEventListContext>(null);
export const TodoListContext = createContext<ITodoListContext>(null);
export const MainPageContext = createContext<IMainPageContext>(null);
export const MonthPageContext = createContext<IMonthPageContext>(null);
export const AppContext = createContext<IGlobalContext>(null);
export const HabitsTrackerContext = createContext<IHabitTrackerContext>(null);

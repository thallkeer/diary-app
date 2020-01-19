import { createContext } from "react";
import {
  IUser,
  IList,
  ITodo,
  IEvent,
  IMainPage,
  ListItem,
  IPage,
  IMonthPage
} from "../models/index";
import { EventThunks } from "../actions/events-actions";
import { TodoThunks } from "../actions/todo-actions";

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

export interface IListState<TListItem extends ListItem, TAction> {
  list: IList<TListItem>;
  loading: boolean;
  dispatch: (action: TAction) => void;
}

export interface IEventListContext extends IListState<IEvent, EventThunks> {}
export interface ITodoListContext extends IListState<ITodo, TodoThunks> {}

export const EventListContext = createContext<IEventListContext>(null);
export const TodoListContext = createContext<ITodoListContext>(null);
export const MainPageContext = createContext<IMainPageContext>(null);
export const MonthPageContext = createContext<IMonthPageContext>(null);
export const AppContext = createContext<IGlobalContext>(null);

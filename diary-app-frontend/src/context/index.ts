import { createContext } from "react";
import { IUser, IList, ITodo, IEvent, IMainPage } from "../models/index";
import { EventThunks } from "../actions/events-actions";

export type GlobalState = {
  user: IUser;
  month: number;
  year: number;
};

export type PageState = {
  page: IMainPage;
};

export type ListState<T extends ITodo | IEvent> = {
  list: IList<T>;
  loading: boolean;
};

export interface IMainPageContext extends PageState {
  events: IEventListContext;
  setPageState: (pageState: IMainPageContext) => void;
  loading: boolean;
}

export interface IEventListContext {
  eventList: ListState<IEvent>;
  dispatch: (action: EventThunks) => void;
}

export const EventListContext = createContext<IEventListContext>(null);
export const MainPageContext = createContext<IMainPageContext>(null);

export interface IMonthPageState extends PageState {}

export const GlobalContext = createContext<GlobalState>({
  month: 1,
  year: 2020,
  user: null
});

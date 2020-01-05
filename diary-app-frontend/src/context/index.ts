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

export interface IMainPageState extends PageState {
  events: EventListStore;
  loading: boolean;
}

export type EventListStore = {
  eventList: ListState<IEvent>;
  dispatch: (action: EventThunks) => void;
};

export const EventListContext = createContext<EventListStore>(null);

export interface IMonthPageState extends PageState {}

export const GlobalContext = createContext<GlobalState>({
  month: 1,
  year: 2020,
  user: null
});

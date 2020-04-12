import { createContext } from "react";
import {
  IList,
  IMainPage,
  ListItem,
  IPage,
  IMonthPage,
  IEventList,
  ITodoList,
  IGoalsArea,
  IUser,
  IHabitsTracker,
} from "../models/index";
import { HabitTrackerThunks } from "./actions/habitTracker-actions";
import { MainPageThunks } from "./actions/mainPage-actions";

export interface IGlobalContext {
  month: number;
  year: number;
  user?: IUser;
  setAppState?: (appState: IGlobalContext) => void;
}

export interface IBasePageState extends PageState<IPage> {}

export type PageState<T extends IPage> = {
  page: T;
  loading: boolean;
};

export interface IMainPageContext
  extends PageState<IMainPage>,
    IDispatchable<MainPageThunks> {
  events: IEventListContext;
}

export interface IMonthPageContext extends PageState<IMonthPage> {}

interface IDispatchable<TAction> {
  dispatch?: (action: TAction) => void;
}

export interface IListState<TList extends IList<ListItem>> {
  list: TList;
  loading: boolean;
  updateListTitle?: (title: string) => void;
  addOrUpdateItem?: (item: ListItem) => void;
  deleteItem?: (itemID: number) => void;
}

export interface IEventListContext extends IListState<IEventList> {}
export interface ITodoListContext extends IListState<ITodoList> {
  toggleTodoItem?: (todoId: number) => void;
  deleteTodoList?: (todoList: ITodoList) => void;
  isDeletable: boolean;
}

export interface IGoalsAreaContext
  extends IDispatchable<HabitTrackerThunks>,
    IGoalsArea {
  addOrUpdate: (tracker?: IHabitsTracker) => void;
  deleteTracker: (tracker: IHabitsTracker) => void;
}

export const EventListContext = createContext<IEventListContext>(null);
export const TodoListContext = createContext<ITodoListContext>(null);
export const MainPageContext = createContext<IMainPageContext>(null);
export const MonthPageContext = createContext<IMonthPageContext>(null);
export const GoalsAreaContext = createContext<IGoalsAreaContext>(null);
export const AppContext = createContext<IGlobalContext>(null);

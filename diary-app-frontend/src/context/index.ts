import { createContext } from "react";
import { IList, ITodoItem, ILightEvent } from "../models/index";

export type BaseState<T extends ITodoItem | ILightEvent> = {
  loading: boolean;
  list: IList<T>;
  dispatch?: (action) => void;
};

export type AppState = {
  events: BaseState<ILightEvent>;
  todos: BaseState<ITodoItem>;
};

export const appInitialState: AppState = {
  events: {
    loading: false,
    list: { id: 0, items: [], month: 0, pageId: 0, title: "" }
  },
  todos: {
    loading: false,
    list: { id: 0, items: [], month: 0, pageId: 0, title: "" }
  }
};

export const Store = createContext<AppState>(appInitialState);

import * as fromTodos from "./todos";
import * as fromEvents from "./events";
import { ThunkDispatch } from "redux-thunk";
import { Action } from "redux";
import { combineReducers } from "redux";

export interface IAppState {
  todos: fromTodos.ITodoReducerState;
  events: fromEvents.IEventReducerState;
}

export type DispatchThunk = ThunkDispatch<IAppState, void, Action>;

export const reducer = combineReducers<IAppState>({
  todos: fromTodos.reducer,
  events: fromEvents.reducer
});

import { IAppState } from "../reducers";
import { createSelector } from "reselect";

const getTodosState = (state: IAppState) => state.todos;

export const getTodos = createSelector(
  [getTodosState],
  s => s.todos
);

export const getTodosLoading = createSelector(
  [getTodosState],
  s => s.loading
);

export const getTodosLoaded = createSelector(
  [getTodosState],
  s => s.loaded
);

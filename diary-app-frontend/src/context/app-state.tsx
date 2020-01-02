import React, { useReducer } from "react";
import { eventsReducer } from "./events";
import { Store } from ".";
import { todosReducer } from "./todos";

export const AppState = ({ children }) => {
  const [eventsState, eventsDispatch] = useReducer(eventsReducer, {
    loading: false,
    list: { id: 0, items: [], month: 0, pageId: 0, title: "" }
  });
  const [todosState, todosDispatch] = useReducer(todosReducer, {
    loading: false,
    list: { id: 0, items: [], month: 0, pageId: 0, title: "" }
  });

  const eventDispatch = action => action(eventsDispatch);
  const todoDispatch = action => action(todosDispatch);

  return (
    <Store.Provider
      value={{
        events: { ...eventsState, dispatch: eventDispatch },
        todos: { ...todosState, dispatch: todoDispatch }
      }}
    >
      {children}
    </Store.Provider>
  );
};

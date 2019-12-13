import React, { useReducer, createContext } from "react";
import { eventsReducer } from "./events";
import { Thunks as eventThunks } from "../actions/events-actions";
import { AppContext } from ".";

export const AppState = ({ children }) => {
  const [state, _dispatch] = useReducer(eventsReducer, {
    events: [],
    loading: false,
    thunks: eventThunks
  });

  const dispatch = action => action(_dispatch);

  return (
    <AppContext.Provider value={{ ...state, dispatch: dispatch }}>
      {children}
    </AppContext.Provider>
  );
};

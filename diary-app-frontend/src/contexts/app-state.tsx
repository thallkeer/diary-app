import React, { useReducer } from "react";
import { eventsReducer } from "./events";
import { Thunks as eventThunks } from "../actions/events-actions";
import { AppContext, appInitialState } from ".";

export const AppState = ({ children }) => {
  const [state, _dispatch] = useReducer(eventsReducer, appInitialState);

  const dispatch = action => action(_dispatch);

  return (
    <AppContext.Provider
      value={{ ...state, dispatch: dispatch, thunks: eventThunks }}
    >
      {children}
    </AppContext.Provider>
  );
};

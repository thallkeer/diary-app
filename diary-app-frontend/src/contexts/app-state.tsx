import React, { useReducer, createContext } from "react";
import { eventsReducer } from "./events";
import { ILightEvent } from "../models";
import EventThunks, { Thunks as eventThunks } from "../actions/events-actions";

export interface IAppContext {
  loading: boolean;
  events: ILightEvent[];
  thunks: EventThunks;
  loadEvents?: (state: IAppContext) => void;
}
export const AppContext = createContext<IAppContext>({
  events: [],
  loading: false,
  thunks: eventThunks
});

export const AppState = ({ children }) => {
  const loadEvents = (state: IAppContext) => {
    console.log(state);
    state.thunks.loadEvents(dispatch);
  };

  const [state, dispatch] = useReducer(eventsReducer, {
    events: [],
    loading: false,
    thunks: eventThunks,
    loadEvents
  });

  return <AppContext.Provider value={state}>{children}</AppContext.Provider>;
};

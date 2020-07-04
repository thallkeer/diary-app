import React, { createContext, useReducer, Dispatch } from "react";
import { IAppState } from ".";
import { appReducer } from "./reducers/app";
import { AppActions } from "./actions/app-actions";

const curDate = new Date();
const initialState: IAppState = {
  month: curDate.getMonth() + 1,
  year: curDate.getFullYear(),
  user: JSON.parse(localStorage.getItem("user")),
  mainPage: null,
  monthPage: null,
  selectedPage: null,
};

interface IAppContextProps {
  state: IAppState;
  dispatch: Dispatch<AppActions>;
}

const store = createContext<IAppContextProps>({
  state: initialState,
  dispatch: () => null,
});
const { Provider } = store;

const StateProvider = ({ children }) => {
  const [state, dispatch] = useReducer(appReducer, initialState);

  return <Provider value={{ state, dispatch }}>{children}</Provider>;
};

export { store, StateProvider };

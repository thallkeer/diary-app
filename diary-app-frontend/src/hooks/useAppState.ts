import { useState, useCallback } from "react";
import { IGlobalContext } from "../context";

export const useAppState = (initialState: IGlobalContext) => {
  const [appState, _setAppState] = useState(initialState);

  const setAppState = useCallback((newState: IGlobalContext): void => {
    const { year, month, user } = newState;
    localStorage.setItem("year", year.toString());
    localStorage.setItem("month", month.toString());
    localStorage.setItem("user", JSON.stringify(user));
    _setAppState({ ...newState });
  }, []);

  return {
    ...appState,
    setAppState
  };
};

import { useState, useCallback } from "react";
import { IGlobalContext } from "../context";

export const useAppState = (initialState: IGlobalContext) => {
  const [appState, _setAppState] = useState(initialState);

  const setAppState = useCallback((newState: IGlobalContext): void => {
    _setAppState({ ...newState });
  }, []);

  return {
    ...appState,
    setAppState
  };
};

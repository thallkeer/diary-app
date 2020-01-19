import { useState, useCallback } from "react";
import { IGlobalContext } from "../context";

export const useAppState = (initialState: IGlobalContext) => {
  const [appState, _setAppState] = useState(initialState);

  const setAppState = useCallback((appState: IGlobalContext): void => {
    _setAppState({ ...appState });
  }, []);

  return {
    ...appState,
    setAppState
  };
};

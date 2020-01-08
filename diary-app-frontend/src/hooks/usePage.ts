import { useState, useCallback } from "react";
import { IMainPageContext, IMonthPageContext } from "../context";

export function usePage<T extends IMainPageContext | IMonthPageContext>(
  initialState: T
): T {
  const [pageState, _setPageState] = useState<T>(initialState);

  const setPageState = useCallback((pageState: T): void => {
    _setPageState({ ...pageState });
  }, []);

  return {
    ...pageState,
    setPageState
  };
}

import { useState, useCallback, useContext, useEffect } from "react";
import { IMainPageContext, IMonthPageContext, AppContext } from "../context";
import { IUser } from "../models";
import axios from "axios";
import { config } from "../helpers/config";

export enum PageType {
  MainPage,
  MonthPage
}

const routeByPageType = new Map([
  [PageType.MainPage, "mainpage"],
  [PageType.MonthPage, "monthpage"]
]);

export function usePage<T extends IMainPageContext | IMonthPageContext>(
  initialState: T,
  pageType: PageType
): T {
  const [pageState, _setPageState] = useState<T>(initialState);
  const { month, year } = useContext(AppContext);
  const user: IUser = JSON.parse(localStorage.getItem("user"));

  const { baseApi, headers } = config;

  useEffect(() => {
    _setPageState({
      ...pageState,
      loading: true
    });

    axios
      .get(
        `${baseApi}${routeByPageType.get(pageType)}/${
          user.id
        }/${year}/${month}`,
        { headers }
      )
      .then(res => {
        _setPageState({ ...pageState, page: res.data, loading: false });
      });
  }, [pageType, year, month]);

  const setPageState = useCallback((pageState: T): void => {
    _setPageState({ ...pageState });
  }, []);

  return {
    ...pageState,
    setPageState
  };
}

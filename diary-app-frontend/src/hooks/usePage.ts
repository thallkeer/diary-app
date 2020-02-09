import { useState, useCallback, useContext, useEffect } from "react";
import { IMainPageContext, IMonthPageContext, AppContext } from "../context";
import { IUser } from "../models";
import axios from "../axios/axios";
import { AxiosError } from "axios";

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
  const query = `${routeByPageType.get(pageType)}/${user.id}/${year}/${month}`;

  useEffect(() => {
    _setPageState(prevState => {
      return {
        ...prevState,
        loading: true
      };
    });

    const getPage = () => {
      return axios.get(query);
    };

    getPage()
      .then(res => {
        _setPageState(prevState => {
          return { ...prevState, page: res.data, loading: false };
        });
      })
      .catch((err: AxiosError) => console.log(err));
  }, [pageType, query, year, month]);

  const setPageState = useCallback((newPageState: T): void => {
    _setPageState({ ...newPageState });
  }, []);

  return {
    ...pageState,
    setPageState
  };
}

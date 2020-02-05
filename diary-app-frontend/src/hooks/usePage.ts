import { useState, useCallback, useContext, useEffect } from "react";
import { IMainPageContext, IMonthPageContext, AppContext } from "../context";
import { IUser } from "../models";
import axios from "../axios/axios";

export enum PageType {
  MainPage,
  MonthPage
}

const routeByPageType = new Map([
  [PageType.MainPage, "mainpage"],
  [PageType.MonthPage, "monthpage"]
]);

type PageParams = {
  userId: string;
  year: number;
  month: number;
};

export function usePage<T extends IMainPageContext | IMonthPageContext>(
  initialState: T,
  pageType: PageType
): T {
  const [pageState, _setPageState] = useState<T>(initialState);
  const { month, year } = useContext(AppContext);
  const user: IUser = JSON.parse(localStorage.getItem("user"));
  const pageApi = `${routeByPageType.get(pageType)}/`;

  const getPage = () => {
    return axios.get(`${pageApi}${user.id}/${year}/${month}`);
  };

  useEffect(() => {
    _setPageState({
      ...pageState,
      loading: true
    });

    getPage().then(res => {
      if (res.data) {
        _setPageState({ ...pageState, page: res.data, loading: false });
      } else {
        createNewPage().then(res =>
          _setPageState({ ...pageState, page: res.data, loading: false })
        );
      }
    });
  }, [pageType, year, month]);

  const setPageState = useCallback((newPageState: T): void => {
    _setPageState({ ...newPageState });
  }, []);

  const createNewPage = () => {
    let data: PageParams = {
      userId: user.id,
      year,
      month
    };
    return axios.post(pageApi + "createNew", data);
  };

  return {
    ...pageState,
    setPageState
  };
}

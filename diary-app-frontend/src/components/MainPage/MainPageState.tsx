import React, { useEffect, useReducer } from "react";
import { MainPageContext } from "../../context";
import { PageType } from "../../context/actions/page-actions";
import { mainPageReducer } from "../../context/mainPage";
import {
  MainPageThunks,
  Thunks as mainPageThunks,
} from "../../context/actions/mainPage-actions";
import { usePage } from "../../hooks/usePage";

export const MainPageState: React.FC = ({ children }) => {
  const { page, loading } = usePage(PageType.MainPage);
  const [pageState, _dispatch] = useReducer(mainPageReducer, {
    page,
    loading,
    events: null,
  });

  const dispatch = (action: MainPageThunks) => action(_dispatch);

  useEffect(() => {
    if (!loading && page !== null) {
      dispatch(mainPageThunks.setPage(page));
      console.log("main page effect ", page);
    }
  }, [page]);

  return (
    <MainPageContext.Provider value={{ ...pageState, dispatch }}>
      {children}
    </MainPageContext.Provider>
  );
};

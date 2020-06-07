import { useContext, useEffect, useReducer } from "react";
import { AppContext } from "../context";
import { IUser } from "../models";
import {
  PageType,
  PageThunks,
  Thunks as pageThunks,
} from "../context/actions/page-actions";
import { pageReducer } from "../context/reducers/page";

export function usePage(pageType: PageType) {
  const [pageState, _dispatch] = useReducer(pageReducer, {
    loading: false,
    page: null,
  });
  const dispatch = (action: PageThunks) => action(_dispatch);
  const { month, year } = useContext(AppContext);
  const user: IUser = JSON.parse(localStorage.getItem("user"));

  useEffect(() => {
    dispatch(pageThunks.loadPage(pageType, user, year, month));
  }, [year, month]);

  return pageState;
}

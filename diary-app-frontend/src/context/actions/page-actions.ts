import { ActionsUnion, createAction } from "./action-helpers";
import axios from "../../axios/axios";
import { IPage, IUser } from "../../models";
import { AxiosError } from "axios";

export const START_LOAD_PAGE = "START_LOAD_PAGE";
export const FINISH_LOAD_PAGE = "FINISH_LOAD_PAGE";

export enum PageType {
  MainPage,
  MonthPage
}

const routeByPageType = new Map([
  [PageType.MainPage, "mainpage"],
  [PageType.MonthPage, "monthpage"]
]);

const Actions = {
  startLoadPage: () => createAction(START_LOAD_PAGE),
  finishLoadPage: (page: IPage) => createAction(FINISH_LOAD_PAGE, page)
};

export const Thunks = {
  loadPage: (pageType: PageType, user: IUser, year: number, month: number) => {
    return dispatch => {
      const query = `${routeByPageType.get(pageType)}/${
        user.id
      }/${year}/${month}`;

      dispatch(Actions.startLoadPage());

      axios
        .get(query)
        .then(res => dispatch(Actions.finishLoadPage(res.data)))
        .catch((err: AxiosError) => console.log(err));
    };
  }
};

export type PageActions = ActionsUnion<typeof Actions>;
export type PageThunks = ActionsUnion<typeof Thunks>;

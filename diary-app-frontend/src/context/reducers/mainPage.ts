import { IMainPageContext } from "..";
import { MainPageActions } from "../actions/mainPage-actions";

export const mainPageReducer = (
  state: IMainPageContext,
  action: MainPageActions
): IMainPageContext => {
  switch (action.type) {
    case "SET_PAGE":
      return { ...state, page: action.payload };

    case "SET_EVENTS":
      return { ...state, events: action.payload };

    default:
      return state;
  }
};

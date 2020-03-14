import { IBasePageState } from ".";
import { PageActions } from "./actions/page-actions";

export const pageReducer = (
  state: IBasePageState,
  action: PageActions
): IBasePageState => {
  switch (action.type) {
    case "START_LOAD_PAGE":
      return { ...state, loading: true };

    case "FINISH_LOAD_PAGE":
      console.log("page loaded", action.payload);

      return { ...state, loading: false, page: action.payload };

    default:
      return state;
  }
};

import { IPageState } from "../..";
import { PageActions } from "../../actions/page-actions";

export const pageReducer = (state: IPageState, action: PageActions) => {
	switch (action.type) {
		case "START_LOAD_PAGE":
			return { ...state, loading: true };

		case "FINISH_LOAD_PAGE":
			return { ...state, loading: false, page: action.payload };

		default:
			return state;
	}
};

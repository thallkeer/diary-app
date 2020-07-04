import { MainPageActions } from "../../actions/mainPage-actions";
import { IMainPage } from "../../../models";

export const mainPageReducer = (
	state: IMainPage,
	action: MainPageActions
): IMainPage => {
	switch (action.type) {
		case "SET_EVENTS":
			return { ...state, events: action.payload };

		case "SET_TODOS":
			return { ...state, todos: action.payload };

		case "SET_PAGE":
			return { ...state, page: action.payload.page };

		default:
			return state;
	}
};

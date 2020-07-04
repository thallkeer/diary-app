import { IAppState } from "..";
import { AppActions } from "../actions/app-actions";

export const appReducer = (state: IAppState, action: AppActions): IAppState => {
	switch (action.type) {
		case "SET_APP_STATE":
			return { ...action.payload };
		case "SET_MAIN_PAGE":
			return {
				...state,
				mainPage: action.payload,
				selectedPage: action.payload,
			};
		case "SET_MONTH_PAGE":
			return {
				...state,
				monthPage: action.payload,
				selectedPage: action.payload,
			};
		case "SET_SELECTED_PAGE":
			return { ...state, selectedPage: action.payload };

		default:
			return state;
	}
};

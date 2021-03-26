import { IUser } from "models/entities";
import { AppActions } from "./app.actions";

export type AppState = {
	month: number;
	year: number;
	user: IUser;
};

const curDate = new Date();
const initialState: AppState = {
	month: curDate.getMonth() + 1,
	year: curDate.getFullYear(),
	user: JSON.parse(localStorage.getItem("user")) as IUser,
};

export const appReducer = (
	state = initialState,
	action: AppActions
): AppState => {
	switch (action.type) {
		case "APP/SET_APP_INFO":
			return { ...state, ...action.payload };
		case "APP/SET_USER":
			return { ...state, user: action.payload };
		case "APP/SET_MONTH":
			return { ...state, month: action.payload };
		default:
			return state;
	}
};

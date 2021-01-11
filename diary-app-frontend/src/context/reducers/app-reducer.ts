import { AxiosResponse } from "axios";
import { toast } from "react-toastify";
import history from "../../components/history";
import { IUser } from "../../models/entities";
import { usersService } from "../../services/users";
import { ActionsUnion, createAction } from "../actions/action-helpers";
import { BaseThunkType } from "../store";
import { IMainPage } from "./page/mainPage-reducer";
import { IMonthPage } from "./page/monthPage-reducer";

export type PageType = IMainPage | IMonthPage;

export type AppState = {
	month: number;
	year: number;
	user: IUser;
	selectedPage: PageType;
};

const curDate = new Date();
const initialState: AppState = {
	month: curDate.getMonth() + 1,
	year: curDate.getFullYear(),
	user: JSON.parse(localStorage.getItem("user")) as IUser,
	selectedPage: null,
};
const SET_SELECTED_PAGE = "APP/SET_SELECTED_PAGE";
const SET_APP_INFO = "APP/SET_APP_INFO";
const SET_USER = "APP/SET_USER";
const SET_MONTH = "APP/SET_MONTH";

export const appReducer = (
	state = initialState,
	action: AppActions
): AppState => {
	switch (action.type) {
		case "APP/SET_SELECTED_PAGE":
			return { ...state, selectedPage: action.payload };
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

export const actions = {
	setSelectedPage: (page: PageType) => createAction(SET_SELECTED_PAGE, page),
	setUser: (user: IUser) => createAction(SET_USER, user),
	setAppInfo: (user: IUser, year: number, month: number) =>
		createAction(SET_APP_INFO, { user, year, month }),
	setMonth: (month: number) => createAction(SET_MONTH, month),
};

export const setMonth = (month: number): ThunkType => async (dispatch) => {
	dispatch(actions.setMonth(month));
};

export const authUser = (
	userAuthModel: IUser,
	signIn: boolean
): ThunkType => async (dispatch) => {
	const authFunc = signIn ? usersService.login : usersService.register;
	await authFunc(userAuthModel)
		.then((user) => {
			dispatch(setUser(user));
			history.push("/");
		})
		.catch((err: AxiosResponse) => {
			toast.error(err.data);
		});
};

export const setUser = (user: IUser): ThunkType => async (dispatch) => {
	dispatch(actions.setUser(user));
};

export const setSelectedPage = (page: PageType): ThunkType => async (
	dispatch
) => {
	dispatch(actions.setSelectedPage(page));
};

type AppActions = ActionsUnion<typeof actions>;
type ThunkType = BaseThunkType<AppActions>;

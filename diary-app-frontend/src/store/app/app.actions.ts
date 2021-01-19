import { BaseThunkType } from "store/state.types";
import { AxiosResponse } from "axios";
import { toast } from "react-toastify";
import history from "../../components/history";
import { IUser } from "../../models/entities";
import { usersService } from "../../services/users";
import { ActionsUnion, createAction } from "../actions/action-helpers";

const SET_APP_INFO = "APP/SET_APP_INFO";
const SET_USER = "APP/SET_USER";
const SET_MONTH = "APP/SET_MONTH";

export const actions = {
	setUser: (user: IUser) => createAction(SET_USER, user),
	setAppInfo: (user: IUser, year: number, month: number) =>
		createAction(SET_APP_INFO, { user, year, month }),
	setMonth: (month: number) => createAction(SET_MONTH, month),
};

export const AppThunks = {
	setMonth: (month: number): AppThunkType => async (dispatch) => {
		dispatch(actions.setMonth(month));
	},

	authUser: (userAuthModel: IUser, signIn: boolean): AppThunkType => async (
		dispatch
	) => {
		const authFunc = signIn ? usersService.login : usersService.register;
		await authFunc(userAuthModel)
			.then((user) => {
				dispatch(actions.setUser(user));
				history.push("/");
			})
			.catch((err: AxiosResponse) => {
				toast.error(err.data);
			});
	},

	setUser: (user: IUser): AppThunkType => async (dispatch) => {
		dispatch(actions.setUser(user));
	},
};

export type AppActions = ActionsUnion<typeof actions>;
export type AppThunkType = BaseThunkType<AppActions>;
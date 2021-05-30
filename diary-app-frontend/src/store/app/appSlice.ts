import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IUser } from "models/entities";
import { AxiosResponse } from "axios";
import { toast } from "react-toastify";
import history from "../../components/history";
import { UserAuthModel, userService } from "../../services/users";
import { AppThunk } from "store/store";

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

const appSlice = createSlice({
	name: "app",
	initialState,
	reducers: {
		setAppInfo(
			state,
			action: PayloadAction<{ user: IUser; year: number; month: number }>
		) {
			return { ...state, ...action.payload };
		},
		setUser(state, action: PayloadAction<IUser>) {
			state.user = action.payload;
		},
		setMonth(state, action: PayloadAction<number>) {
			state.month = action.payload;
		},
		setYear(state, action: PayloadAction<number>) {
			state.year = action.payload;
		},
	},
});

export const setMonth =
	(month: number): AppThunk =>
	(dispatch) => {
		dispatch(appSlice.actions.setMonth(month));
	};

export const setYear =
	(year: number): AppThunk =>
	(dispatch) => {
		dispatch(appSlice.actions.setYear(year));
	};

export const authUser =
	(userAuthModel: UserAuthModel, signIn: boolean): AppThunk =>
	async (dispatch) => {
		const authFunc = signIn ? userService.login : userService.register;
		return authFunc(userAuthModel)
			.then((user) => {
				dispatch(appSlice.actions.setUser(user));
				history.push("/");
			})
			.catch((err: AxiosResponse) => {
				if (err && err.data) {
					const errors = err.data.errors;
					toast.error(Object.values(errors).toString());
				}
			});
	};

export const setUser =
	(user: IUser): AppThunk =>
	(dispatch) => {
		dispatch(appSlice.actions.setUser(user));
	};

export const appReducer = appSlice.reducer;

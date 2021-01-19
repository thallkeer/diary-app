import { IUser } from "models/entities";
import { IMonthPage } from "models/Pages/pages";
import { PageThunkType, Thunks } from "./pages.actions";

export const loadMonthPage = (
	user: IUser,
	year: number,
	month: number
): PageThunkType => async (dispatch) => {
	dispatch(Thunks.loadPage<IMonthPage>("monthPage", user, year, month));
};

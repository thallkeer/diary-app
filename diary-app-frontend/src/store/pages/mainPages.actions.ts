import { IUser } from "models/entities";
import { IMainPage } from "models/Pages/pages";
import { PageThunkType, Thunks } from "./pages.actions";

export const loadMainPage = (
	user: IUser,
	year: number,
	month: number
): PageThunkType => async (dispatch) => {
	dispatch(Thunks.loadPage<IMainPage>("mainPage", user, year, month));
};

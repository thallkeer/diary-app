import { AppStateType } from "store/reducer";
import { useDispatch, useSelector } from "react-redux";
import { IUser } from "models/entities";
import { IPage } from "models/Pages/pages";
import { getAppInfo } from "selectors/app-selectors";
import { useEffect } from "react";

export const usePage = <TPage extends IPage>(
	pageSelector: (state: AppStateType) => TPage,
	loadPageFunc: (user: IUser, year: number, month: number) => void
) => {
	const { user, year, month } = useSelector(getAppInfo);
	const page = useSelector(pageSelector);
	const dispatch = useDispatch();
	useEffect(() => {
		dispatch(loadPageFunc(user, year, month));
	}, [loadPageFunc, user, year, month]);

	return page;
};

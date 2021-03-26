import { AppStateType } from "store/reducer";
import { useDispatch, useSelector } from "react-redux";
import { IUser } from "models/entities";
import { IPage } from "models/Pages/pages";
import { getAppInfo } from "selectors/app-selectors";
import { useEffect } from "react";
import { PageComponent } from "store/pages";

export const usePage = <TPage extends IPage>(
	pageSelector: (state: AppStateType) => TPage,
	pageComponent: PageComponent<TPage>
) => {
	const { user, year, month } = useSelector(getAppInfo);
	const page = useSelector(pageSelector);
	const dispatch = useDispatch();
	useEffect(() => {
		dispatch(pageComponent.loadPage(user, year, month));
	}, [pageComponent, user, year, month]);

	return page;
};

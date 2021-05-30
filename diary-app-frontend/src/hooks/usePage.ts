import { useSelector } from "react-redux";
import { IPage } from "models/Pages/pages";
import { getAppInfo } from "selectors/app-selectors";
import { useEffect } from "react";
import { PageComponent } from "store/pages";
import { useAppDispatch, useAppSelector } from "./hooks";
import { IPageState } from "models/states";
import { RootState } from "store/store";

export const usePage = <TPage extends IPage, TState extends IPageState<TPage>>(
	pageSelector: (state: RootState) => TState,
	pageComponent: PageComponent<TPage, TState>
) => {
	const { user, year, month } = useSelector(getAppInfo);
	const page = useAppSelector(pageSelector);
	const dispatch = useAppDispatch();
	useEffect(() => {
		dispatch(pageComponent.loadPage({ user, year, month }));
	}, [pageComponent, user, year, month, dispatch]);

	return page;
};

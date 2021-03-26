import { IMainPage, IMonthPage, IPage } from "models";
import { IPageArea } from "models/PageAreas/pageAreas";
import { IPageAreaState } from "models/states";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { PageAreaComponent } from "store/pageAreas";
import { AppStateType } from "store/reducer";

import { getAppInfo } from "../selectors/app-selectors";
import { getMainPage, getMonthPage } from "../store/pages/pages.selectors";

export const useMonthPageArea = <
	TAreaState extends IPageAreaState<TArea>,
	TArea extends IPageArea
>(
	areaSelector: (state: AppStateType) => TAreaState,
	pageAreaComponent: PageAreaComponent<IMonthPage, TArea>
) => {
	const { page, area, isLoading } = usePageArea(
		getMonthPage,
		areaSelector,
		pageAreaComponent
	);
	return {
		monthPage: page,
		area,
		isLoading,
	};
};

export const useMainPageArea = <
	TAreaState extends IPageAreaState<TArea>,
	TArea extends IPageArea
>(
	areaSelector: (state: AppStateType) => TAreaState,
	pageAreaComponent: PageAreaComponent<IMainPage, TArea>
) => {
	const { page, area, isLoading } = usePageArea(
		getMainPage,
		areaSelector,
		pageAreaComponent
	);
	return {
		mainPage: page,
		area,
		isLoading,
	};
};

export const usePageArea = <
	TPage extends IPage,
	TAreaState extends IPageAreaState<TArea>,
	TArea extends IPageArea
>(
	pageSelector: (state: AppStateType) => TPage,
	areaSelector: (state: AppStateType) => TAreaState,
	pageAreaComponent: PageAreaComponent<TPage, TArea>
) => {
	const dispatch = useDispatch();
	const page = useSelector(pageSelector);
	const app = useSelector(getAppInfo);
	const { user, year, month } = app;
	const { area, isLoading } = useSelector(areaSelector) as TAreaState;

	useEffect(() => {
		if (page) {
			dispatch(pageAreaComponent.loadPageArea(page.id));
		}
	}, [pageAreaComponent, page, user, year, month]);

	return {
		page,
		area,
		isLoading: !page || isLoading || !area,
	};
};

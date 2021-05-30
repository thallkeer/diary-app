import { AsyncThunk } from "@reduxjs/toolkit";
import { IPage } from "models";
import { IPageArea } from "models/PageAreas/pageAreas";
import { IPageState, PageAreaState } from "models/states";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { AppThunk, RootState } from "store/store";

import { getAppInfo } from "../selectors/app-selectors";
import { getMainPage, getMonthPage } from "../selectors/pages.selectors";

export const useMonthPageArea = <
	TAreaState extends PageAreaState<TArea>,
	TArea extends IPageArea
>(
	areaSelector: (state: RootState) => TAreaState,
	loadPageArea: AsyncThunk<TArea, number, {}>,
	pageAreaLoaded: (area: TArea) => AppThunk
) => {
	const { page, area, status } = usePageArea(
		getMonthPage,
		areaSelector,
		loadPageArea,
		pageAreaLoaded
	);
	return {
		monthPage: page,
		area,
		status,
	};
};

export const useMainPageArea = <
	TAreaState extends PageAreaState<TArea>,
	TArea extends IPageArea
>(
	areaSelector: (state: RootState) => TAreaState,
	loadPageArea: AsyncThunk<TArea, number, {}>,
	pageAreaLoaded: (area: TArea) => AppThunk
) => {
	const { page, area, status } = usePageArea(
		getMainPage,
		areaSelector,
		loadPageArea,
		pageAreaLoaded
	);
	return {
		mainPage: page,
		area,
		status,
	};
};

export const usePageArea = <
	TPageState extends IPageState<TPage>,
	TPage extends IPage,
	TAreaState extends PageAreaState<TArea>,
	TArea extends IPageArea
>(
	pageSelector: (state: RootState) => TPageState,
	areaSelector: (state: RootState) => TAreaState,
	loadPageArea: AsyncThunk<TArea, number, {}>,
	pageAreaLoaded: (area: TArea) => AppThunk
) => {
	const dispatch = useDispatch();
	const pageState = useSelector(pageSelector);
	const page = pageState.page;
	const pageStatus = pageState.status;
	const { area, status } = useSelector(areaSelector);
	const [loadedCalled, setLoadedCalled] = useState(false);

	useEffect(() => {
		if (pageStatus === "succeeded" && page) {
			dispatch(loadPageArea(page.id));
			setLoadedCalled(false);
		}
	}, [loadPageArea, pageStatus, page, dispatch]);

	useEffect(() => {
		if (!loadedCalled && area && status === "succeeded") {
			dispatch(pageAreaLoaded(area));
			setLoadedCalled(true);
		}
	}, [dispatch, pageStatus, area, status, pageAreaLoaded, loadedCalled]);

	return {
		area,
		status,
		page,
	};
};

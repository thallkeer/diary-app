import { AsyncThunk } from "@reduxjs/toolkit";
import { IPage } from "models";
import { IPageArea } from "models/PageAreas/pageAreas";
import { IPageState, PageAreaState } from "models/states";
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { RootState } from "store/store";
import { getMainPage, getMonthPage } from "../selectors/pages.selectors";
import { useAppSelector } from "./hooks";

export const useMonthPageArea = <
	TAreaState extends PageAreaState<TArea>,
	TArea extends IPageArea
>(
	areaSelector: (state: RootState) => TAreaState,
	loadPageArea: AsyncThunk<TArea, number, {}>
) => {
	const { page, area, status } = usePageArea(
		getMonthPage,
		areaSelector,
		loadPageArea
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
	loadPageArea: AsyncThunk<TArea, number, {}>
) => {
	const { page, area, status } = usePageArea(
		getMainPage,
		areaSelector,
		loadPageArea
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
	loadPageArea: AsyncThunk<TArea, number, {}>
) => {
	const dispatch = useDispatch();
	const pageState = useAppSelector(pageSelector);
	const page = pageState.page;
	const pageStatus = pageState.status;
	const { area, status: areaStatus } = useAppSelector(areaSelector);

	useEffect(() => {
		if (pageStatus === "succeeded" && page) {
			console.log("First effect ", pageStatus, page);
			dispatch(loadPageArea(page.id));
		}
	}, [loadPageArea, pageStatus, page, dispatch]);

	return {
		area,
		status: areaStatus,
		page,
	};
};

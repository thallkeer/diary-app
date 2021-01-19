import { IPageArea } from "models/PageAreas/pageAreas";
import { Dispatch, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { IPageAreaState } from "store/pageAreas";
import { AppStateType } from "store/reducer";

import { getAppInfo } from "../selectors/app-selectors";
import { getMonthPage } from "../store/pages/pages.selectors";

export const usePageArea = <
	TAreaState extends IPageAreaState<TArea>,
	TArea extends IPageArea
>(
	areaSelector: (state: AppStateType) => TAreaState,
	loadPageArea: (dispatch: Dispatch<any>, pageId: number) => void
) => {
	const dispatch = useDispatch();
	const monthPage = useSelector(getMonthPage);
	const app = useSelector(getAppInfo);
	const { user, year, month } = app;
	const { area, isLoading } = useSelector(areaSelector) as TAreaState;

	useEffect(() => {
		if (monthPage) {
			loadPageArea(dispatch, monthPage.id);
		}
	}, [monthPage, user, year, month]);

	return {
		monthPage,
		area,
		isLoading: !monthPage || isLoading || !area,
	};
};

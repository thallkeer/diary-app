import React, { useEffect, useContext, useReducer, Reducer } from "react";
import { IPageArea } from "../../models";
import usePageArea, { PageAreaState } from "../../hooks/usePageArea";
import { store } from "../../context/store";

// interface MonthAreaProps<T extends IPageArea>
// 	extends React.HTMLAttributes<HTMLDivElement> {
// 	areaName: string;
// 	areaBody: (areaProps: PageAreaState<T>) => JSX.Element;
// }

export function useMonthArea<T extends IPageArea, TAction>(
	areaName: string,
	reducer: Reducer<PageAreaState<T>, TAction>,
	onLoad: (area: T) => TAction
): [PageAreaState<T>, React.Dispatch<TAction>] {
	const pageArea = usePageArea<T>(areaName);
	const { area, loading } = pageArea;
	const { selectedPage } = useContext(store).state;

	const [state, dispatch] = useReducer(reducer, pageArea);

	useEffect(() => {
		if (area && !loading) {
			dispatch(onLoad(area));
		}
	}, [area, loading, selectedPage, onLoad]);

	return [state, dispatch];
}

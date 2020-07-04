import { useState, useEffect, useContext } from "react";
import { IPageArea } from "../models";
import { store } from "../context/store";
import { getSelectedPage } from "../selectors";
import { getPageArea } from "../context/actions/monthPage-actions";

export type PageAreaState<T extends IPageArea> = {
	loading: boolean;
	area: T;
};

export default function usePageArea<T extends IPageArea>(
	areaName: string
): PageAreaState<T> {
	const app = useContext(store).state;
	const { monthPage } = app;
	const page = getSelectedPage(app);
	const [state, setState] = useState<PageAreaState<T>>({
		loading: false,
		area: null,
	});

	useEffect(() => {
		let isUnmounting = false;
		const fetchPageArea = async () => {
			if (!isUnmounting) {
				setState((prevState) => {
					return { ...prevState, loading: true };
				});
				const pageArea = await getPageArea<T>(areaName, page.id);
				setState((prevState) => {
					return { ...prevState, area: pageArea.data, loading: false };
				});
			}
		};

		if (page && monthPage && page === monthPage.page) {
			fetchPageArea();
		}
		return () => (isUnmounting = true);
	}, [page, monthPage, areaName]);

	return state;
}

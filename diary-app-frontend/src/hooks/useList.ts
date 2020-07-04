import { List } from "../models";
import { getSelectedPage } from "../selectors";
import { useContext, useEffect } from "react";
import { store } from "../context/store";

export function useList<T extends List>(
	currentList: T,
	onLoad: (pageID: number) => void
) {
	const app = useContext(store);
	const page = getSelectedPage(app.state);

	useEffect(() => {
		let isUnmounting = false;
		const needLoadList = () => !isUnmounting && page && !currentList;

		if (needLoadList()) {
			onLoad(page.id);
		}

		return () => (isUnmounting = true);
	}, [page, currentList]);
}

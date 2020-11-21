import { List } from "../models";
import { getSelectedPage } from "../selectors";
import { useEffect } from "react";
import { useSelector } from "react-redux";

export function useList<T extends List>(
	currentList: T,
	onLoad: (pageID: number) => void
) {
	const page = useSelector(getSelectedPage);

	useEffect(() => {
		let isUnmounting = false;
		const needLoadList = () => !isUnmounting && page && !currentList;
		console.log("useList", page, currentList);
		if (needLoadList()) {
			console.log("load list for page", page, currentList);

			onLoad(page.id);
		}

		return () => (isUnmounting = true);
	}, [page, currentList]);
}

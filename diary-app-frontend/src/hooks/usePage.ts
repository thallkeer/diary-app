import { useContext, useEffect, useReducer } from "react";
import { loadPage } from "../context/actions/page-actions";
import { pageReducer } from "../context/reducers/page/page";
import { store } from "../context/store";

export function usePage(pageName: string) {
	const [state, dispatch] = useReducer(pageReducer, {
		loading: false,
		page: null,
	});
	const { year, month, user } = useContext(store).state;

	useEffect(() => {
		let userFromState = user;
		if (!userFromState)
			userFromState = JSON.parse(localStorage.getItem("user"));
		loadPage(pageName, user, year, month, dispatch);
	}, [pageName, year, month, user]);

	return state;
}

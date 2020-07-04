import React, { useContext, useEffect, useReducer } from "react";
import { usePage } from "../../hooks/usePage";
import { Actions as appActions } from "../../context/actions/app-actions";
import { store } from "../../context/store";
import { IMonthPage } from "../../models";
import { MonthPageActions } from "../../context/actions/monthPage-actions";
import { monthPageReducer } from "../../context/reducers/page/monthPage";

interface IMonthPageContext {
	monthPageState: IMonthPage;
	dispatch: React.Dispatch<MonthPageActions>;
}

const monthPageContext = React.createContext<IMonthPageContext>(null);
const { Provider } = monthPageContext;

const MonthPageState: React.FC = ({ children }) => {
	const { loading, page } = usePage("monthPage");
	const app = useContext(store);
	const { monthPage, mainPage } = app.state;
	const [state, dispatch] = useReducer(monthPageReducer, {
		page,
		desiresArea: null,
		goalsArea: null,
		ideasArea: null,
		purchasesArea: null,
	});

	useEffect(() => {
		if (!loading && page && (!monthPage || state.page !== monthPage.page)) {
			app.dispatch(
				appActions.setMonthPage({
					page,
					desiresArea: null,
					goalsArea: null,
					ideasArea: null,
					purchasesArea: null,
				})
			);
		}
		return () => app.dispatch(appActions.setSelectedPage(mainPage));
	}, [page, loading]);

	return (
		<Provider value={{ monthPageState: state, dispatch: dispatch }}>
			{children}
		</Provider>
	);
};

export { monthPageContext, MonthPageState };

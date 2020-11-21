import React, { useContext, useEffect, useReducer } from "react";
// import { usePage } from "../../hooks/usePage";
// import { IMonthPage } from "../../models";
// import {
// 	MonthPageActions,
// 	Actions as actions,
// } from "../../context/actions/monthPage-actions";
// import { monthPageReducer } from "../../context/reducers/page/monthPage-reducer";

// interface IMonthPageContext {
// 	monthPageState: IMonthPage;
// 	dispatch: React.Dispatch<MonthPageActions>;
// }

// const monthPageContext = React.createContext<IMonthPageContext>(null);
// const { Provider } = monthPageContext;

// const MonthPageState: React.FC = ({ children }) => {
// 	const { loading, page } = usePage("monthPage");
// 	const app = useContext(store);
// 	const { monthPage, mainPage } = app.state;
// 	const [state, dispatch] = useReducer(monthPageReducer, {
// 		page,
// 	});

// 	useEffect(() => {
// 		if (!loading && page && (!monthPage || state.page !== monthPage.page)) {
// 			const loadedPage: IMonthPage = { page };
// 			app.dispatch(appActions.setMonthPage(loadedPage));
// 			dispatch(actions.setPage(loadedPage));
// 		}
// 		return () => app.dispatch(appActions.setSelectedPage(mainPage));
// 	}, [page, loading]);

// 	return (
// 		<Provider value={{ monthPageState: state, dispatch: dispatch }}>
// 			{children}
// 		</Provider>
// 	);
// };

// export { monthPageContext, MonthPageState };

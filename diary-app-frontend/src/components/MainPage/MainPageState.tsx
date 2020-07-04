import React, { useEffect, useReducer, createContext, useContext } from "react";
import { mainPageReducer } from "../../context/reducers/page/mainPage";
import {
	MainPageActions,
	Actions as mainPageActions,
} from "../../context/actions/mainPage-actions";
import { Actions as appActions } from "../../context/actions/app-actions";
import { usePage } from "../../hooks/usePage";
import { store } from "../../context/store";
import { IMainPage } from "../../models";

export interface IMainPageState {
	mainPage: IMainPage;
	loading: boolean;
}

export interface IMainPageContext {
	state: IMainPageState;
	dispatch: React.Dispatch<MainPageActions>;
}

const initialState: IMainPageState = {
	mainPage: null,
	loading: false,
};

const mainPageContext = createContext<IMainPageContext>({
	state: initialState,
	dispatch: null,
});

const { Provider } = mainPageContext;

const MainPageState: React.FC = ({ children }) => {
	const { page, loading } = usePage("mainPage");
	const app = useContext(store);

	const [state, dispatch] = useReducer(mainPageReducer, {
		page,
		events: null,
		todos: null,
	});

	useEffect(() => {
		if (!loading && page && state !== app.state.mainPage) {
			const mainPage: IMainPage = { page, events: null, todos: null };
			app.dispatch(appActions.setMainPage(mainPage));
			dispatch(mainPageActions.setPage(mainPage));
		}

		return () => app.dispatch(appActions.setSelectedPage(app.state.monthPage));
	}, [page]);

	return (
		<Provider
			value={{
				state: {
					mainPage: state,
					loading,
				},
				dispatch,
			}}
		>
			{children}
		</Provider>
	);
};

export { mainPageContext, MainPageState };

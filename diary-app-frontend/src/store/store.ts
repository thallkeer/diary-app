import { AnyAction, Middleware, Dispatch, Action } from "redux";
import logger from "redux-logger";
import { createBrowserHistory } from "history";
import { routerMiddleware as createRouterMiddleware } from "connected-react-router";
import { rootReducer } from "./reducer";
import LogRocket from "logrocket";
import { configureStore, ThunkAction } from "@reduxjs/toolkit";

export const history = createBrowserHistory();
const routerMiddleware = createRouterMiddleware(history);

const development: boolean =
	!process.env.NODE_ENV || process.env.NODE_ENV === "development";

if (!development) {
	LogRocket.init(process.env.REACT_APP_LOG_ROCKET_API_KEY);
}
const loggerMiddleware: Middleware<{}, any, Dispatch<AnyAction>> = development
	? logger
	: LogRocket.reduxMiddleware();

const store = configureStore({
	reducer: rootReducer,
	middleware: (getDefaultMiddleware) =>
		getDefaultMiddleware().concat(routerMiddleware, loggerMiddleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export type AppThunk<A extends Action<string> = Action, R = void> = ThunkAction<
	R,
	RootState,
	unknown,
	A
>;

// @ts-ignore
window.__store__ = store;

export default store;

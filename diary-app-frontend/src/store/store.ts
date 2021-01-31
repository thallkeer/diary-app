import { applyMiddleware, compose, createStore } from "redux";
import logger from "redux-logger";
import thunkMiddleware from "redux-thunk";
import { createBrowserHistory } from "history";
import { routerMiddleware as createRouterMiddleware } from "connected-react-router";
import { rootReducer } from "./reducer";
import LogRocket from "logrocket";

export type InferActionsTypes<T> = T extends {
	[keys: string]: (...args: any[]) => infer U;
}
	? U
	: never;

// @ts-ignore
const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

export const history = createBrowserHistory();
const routerMiddleware = createRouterMiddleware(history);

const development: boolean =
	!process.env.NODE_ENV || process.env.NODE_ENV === "development";

if (!development) LogRocket.init(process.env.LOG_ROCKET_API_KEY);
const loggerMiddleware = development ? logger : LogRocket.reduxMiddleware();

const middlewares = [thunkMiddleware, routerMiddleware, loggerMiddleware];

const store = createStore(
	rootReducer,
	composeEnhancers(applyMiddleware(...middlewares))
);
// @ts-ignore
window.__store__ = store;

export default store;

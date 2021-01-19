import { applyMiddleware, compose, createStore } from "redux";
import logger from "redux-logger";
import thunkMiddleware from "redux-thunk";
import { createBrowserHistory } from "history";
import { routerMiddleware as createRouterMiddleware } from "connected-react-router";
import { rootReducer } from "./reducer";

export type InferActionsTypes<T> = T extends {
	[keys: string]: (...args: any[]) => infer U;
}
	? U
	: never;

// @ts-ignore
const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

export const history = createBrowserHistory();
const routerMiddleware = createRouterMiddleware(history);

const middlewares = [thunkMiddleware, routerMiddleware, logger];

const store = createStore(
	rootReducer,
	composeEnhancers(applyMiddleware(...middlewares))
);
// @ts-ignore
window.__store__ = store;

export default store;

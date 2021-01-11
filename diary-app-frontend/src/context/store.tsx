import { appReducer } from "./reducers/app-reducer";
import {
	Action,
	applyMiddleware,
	combineReducers,
	compose,
	createStore,
} from "redux";
import logger from "redux-logger";
import thunkMiddleware, { ThunkAction } from "redux-thunk";
import { createBrowserHistory } from "history";
import { routerMiddleware as createRouterMiddleware } from "connected-react-router";
import { mainPageReducer } from "./reducers/page/mainPage-reducer";
import { monthPageReducer } from "./reducers/page/monthPage-reducer";

let rootReducer = combineReducers({
	app: appReducer,
	mainPage: mainPageReducer,
	monthPage: monthPageReducer,
});

type RootReducerType = typeof rootReducer;
export type AppStateType = ReturnType<RootReducerType>;

export type InferActionsTypes<T> = T extends {
	[keys: string]: (...args: any[]) => infer U;
}
	? U
	: never;

export type BaseThunkType<
	A extends Action = Action,
	R = Promise<void>
> = ThunkAction<R, AppStateType, unknown, A>;

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

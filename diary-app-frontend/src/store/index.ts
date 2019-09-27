import { logger } from "redux-logger";
import { createStore, applyMiddleware } from "redux";
import { reducer } from "../reducers";
import thunk from "redux-thunk";

const middlewares = applyMiddleware(thunk, logger);

const store = createStore(reducer, {}, middlewares);

export default store;

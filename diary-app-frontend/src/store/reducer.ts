import { combineReducers } from "redux";
import { appReducer } from "./app/app.reducer";
import { mainPageReducer } from "./pages/mainPages.reducer";
import { monthPageReducer } from "./pages/monthPages.reducer";

export const rootReducer = combineReducers({
	app: appReducer,
	mainPage: mainPageReducer,
	monthPage: monthPageReducer,
});

type RootReducerType = typeof rootReducer;
export type AppStateType = ReturnType<RootReducerType>;

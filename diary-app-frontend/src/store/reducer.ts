import { appReducer } from "./app/appSlice";
import { mainPageReducer } from "./pages/mainPages.reducer";
import { monthPageReducer } from "./pages/monthPages.reducer";
import { combineReducers } from "@reduxjs/toolkit";

export const rootReducer = combineReducers({
	app: appReducer,
	mainPage: mainPageReducer,
	monthPage: monthPageReducer,
});

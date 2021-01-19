import { combineReducers } from "redux";
import { appReducer } from "./app/app.reducer";
import { goalsAreaReducer } from "./pageAreas/goals/goalsArea.reducer";
import { mainPageReducer } from "./pages/mainPages.reducer";
import { monthPageReducer } from "./pages/monthPages.reducer";

export const rootReducer = combineReducers({
	app: appReducer,
	mainPage: mainPageReducer,
	monthPage: monthPageReducer,
	goalsArea: goalsAreaReducer,
});

type RootReducerType = typeof rootReducer;
export type AppStateType = ReturnType<RootReducerType>;

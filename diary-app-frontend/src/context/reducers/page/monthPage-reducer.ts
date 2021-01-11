import { combineReducers } from "redux";
import { IPage, IUser } from "../../../models/entities";
import { IPageState } from "../../../models/states";
import { desiresAreaReducer } from "../pageArea/desiresArea-reducer";
import { goalsAreaReducer } from "../pageArea/goalsArea-reducer";
import { ideasAreaReducer } from "../pageArea/ideasArea-reducer";
import { purchasesAreaReducer } from "../pageArea/purchasesArea-reducer";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import {
	createNamedWrapperPageReducer,
	loadPage,
	PageThunkType,
} from "./page-reducer";

export interface IMonthPage extends IPage {}

export interface IMonthPageState extends IPageState<IMonthPage> {}

const initialState: IMonthPageState = {
	pageName: "monthPage",
	page: null,
	...INITIAL_LOADABLE_STATE,
};

export const monthPageReducer = combineReducers({
	page: createNamedWrapperPageReducer<IMonthPage>(initialState, "monthPage"),
	purchasesArea: purchasesAreaReducer,
	desiresArea: desiresAreaReducer,
	ideasArea: ideasAreaReducer,
	goalsArea: goalsAreaReducer,
});

export const loadMonthPage = (
	user: IUser,
	year: number,
	month: number
): PageThunkType => async (dispatch) => {
	dispatch(loadPage<IMonthPage>("monthPage", user, year, month));
};

// export const monthPageReducer = (
// 	state = initialState,
// 	action: MonthPageActions
// ): IMonthPageState => {
// 	switch (action.type) {
// 		case "MONTH_PAGE/SET_DESIRES_AREA":
// 		case "MONTH_PAGE/SET_GOALS_AREA":
// 		case "MONTH_PAGE/SET_IDEAS_AREA":
// 		case "MONTH_PAGE/SET_PURCHASES_AREA":
// 		case "MONTH_PAGE/SET_PAGE":
// 			return { ...state, ...action.payload };

// 		case "MONTH_PAGE/SET_PAGE_AREA":
// 			console.log("MONTH_PAGE/SET_PAGE_AREA payload: ", action.payload);
// 			const newState = { ...state, ...action.payload };
// 			console.log("MONTH_PAGE/SET_PAGE_AREA newState: ", newState);
// 			return { ...state, ...action.payload };

// 		default:
// 			return state;
// 	}
// };

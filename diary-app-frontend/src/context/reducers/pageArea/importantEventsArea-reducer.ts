import {  IPageAreaState } from "../../../models";
// import { ActionsUnion } from "../../actions/action-helpers";
// import { BaseThunkType } from "../../store";
// import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
// import {
// 	createNamedWrapperPageAreaReducer,
// 	loadPageArea,
// } from "./pageArea-reducer";

// export interface IImportantEventsAreaState
// 	extends IPageAreaState<IImportantEventsArea> {}

// const initialState: IImportantEventsAreaState = {
// 	area: null,
// 	pageAreaName: "importantEventsArea",
// 	...INITIAL_LOADABLE_STATE,
// };

// export const importantEventsAreaReducer = createNamedWrapperPageAreaReducer(
// 	initialState,
// 	initialState.pageAreaName
// );

// const actions = {};

// export const loadImportantEventsArea = (pageID: number): ThunkType => async (
// 	dispatch
// ) => {
// 	dispatch(
// 		loadPageArea<IImportantEventsArea>(
// 			initialState.pageAreaName,
// 			"mainPage",
// 			pageID
// 		)
// 	);
// };

// export type ImportantEventsAreaActions = ActionsUnion<typeof actions>;
// type ThunkType = BaseThunkType<ImportantEventsAreaActions>;

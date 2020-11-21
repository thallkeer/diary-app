 import { IPageAreaState, ITodoList } from "../../../models";
// import {
// 	loadPageArea,
// 	PageAreaActions,
// 	pageAreaReducer,
// } from "./pageArea-reducer";
// import { getPurchasesLists } from "../../../selectors";
// import { ActionsUnion, createAction } from "../../actions/action-helpers";
// import axiosInstance from "../../../axios/axios";
// import { BaseThunkType } from "../../store";

// const ADD_PURCHASES_LIST = "ADD_PURCHASES_LIST";
// const DELETE_PURCHASES_LIST = "DELETE_PURCHASES_LIST";

// export interface IPurchasesAreaState extends IPageAreaState<IPurchasesArea> {}

// export const purchasesAreaReducer = (
// 	state: IPurchasesAreaState,
// 	action: PurchasesAreaActions
// ): IPurchasesAreaState => {
// 	const purchasesArea = state.area;
// 	const purchasesLists = getPurchasesLists(state);

// 	switch (action.type) {
// 		case "ADD_PURCHASES_LIST": {
// 			return {
// 				...state,
// 				area: {
// 					...purchasesArea,
// 					purchasesLists: [...purchasesLists, action.payload],
// 				},
// 			};
// 		}

// 		case "DELETE_PURCHASES_LIST": {
// 			return {
// 				...state,
// 				area: {
// 					...purchasesArea,
// 					purchasesLists: purchasesLists.filter(
// 						(pl) => pl.id !== action.payload
// 					),
// 				},
// 			};
// 		}

// 		default:
// 			return pageAreaReducer<IPurchasesArea, IPurchasesAreaState>(
// 				state,
// 				action
// 			);
// 	}
// };

// const purcshasesAreaActions = {
// 	addList: (purchasesList: ITodoList) =>
// 		createAction(ADD_PURCHASES_LIST, purchasesList),
// 	deleteList: (purchasesListId: number) =>
// 		createAction(DELETE_PURCHASES_LIST, purchasesListId),
// };

// export const loadPurchasesArea = (pageID: number): ThunkType => async (
// 	dispatch
// ) => {
// 	dispatch(loadPageArea<IPurchasesArea>("purchasesArea", "monthPage", pageID));
// };

// export async function addPurchasesList(
// 	purchasesList: ITodoList,
// 	dispatch: React.Dispatch<PurchasesAreaActions>
// ) {
// 	if (!purchasesList) return;

// 	axiosInstance.post("todo", purchasesList).then((res) => {
// 		purchasesList.id = res.data;
// 		dispatch(purcshasesAreaActions.addList(purchasesList));
// 	});
// }

// export async function deletePurchasesList(
// 	purchasesList: ITodoList,
// 	dispatch: React.Dispatch<PurchasesAreaActions>
// ) {
// 	if (!purchasesList) return;
// 	dispatch(purcshasesAreaActions.deleteList(purchasesList.id));
// }

// export type PurchasesAreaActions = ActionsUnion<typeof purcshasesAreaActions>;
// type ThunkType = BaseThunkType<PurchasesAreaActions & PageAreaActions>;

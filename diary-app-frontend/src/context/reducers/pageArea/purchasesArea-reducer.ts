import { IPageArea, IPageAreaState, ITodoList } from "../../../models";
import {
	getPageAreaActions,
	loadPageArea,
	pageAreaReducer,
} from "./pageArea-reducer";
import { ActionsUnion, createAction } from "../../actions/action-helpers";
import { BaseThunkType } from "../../store";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import { todosService } from "../../../services/todosService";
import { todoActions } from "../list/todos";

const ADD_PURCHASES_LIST = "ADD_PURCHASES_LIST";
const DELETE_PURCHASES_LIST = "DELETE_PURCHASES_LIST";

interface IPurchasesArea extends IPageArea {
	purchasesLists: IPurchasesList[];
}

interface IPurchasesList {
	id: number;
	list: ITodoList;
}

export interface IPurchasesAreaState extends IPageAreaState<IPurchasesArea> {}

const initialState: IPurchasesAreaState = {
	area: null,
	pageAreaName: "purchasesArea",
	...INITIAL_LOADABLE_STATE,
};

export const PURCHASES_LIST = "purchasesList";

export const purchasesAreaReducer = (
	state = initialState,
	action: PurchasesAreaActions
): IPurchasesAreaState => {
	const purchasesArea = state.area;
	const purchasesLists = purchasesArea?.purchasesLists;

	switch (action.type) {
		case "ADD_PURCHASES_LIST": {
			return {
				...state,
				area: {
					...purchasesArea,
					purchasesLists: [...purchasesLists, action.payload],
				},
			};
		}

		case "DELETE_PURCHASES_LIST": {
			return {
				...state,
				area: {
					...purchasesArea,
					purchasesLists: purchasesLists.filter(
						(pl) => pl.id !== action.payload
					),
				},
			};
		}

		default:
			return pageAreaReducer<IPurchasesArea, IPurchasesAreaState>(
				state,
				action
			);
	}
};

export const loadPurchasesArea = (pageID: number): ThunkType => async (
	dispatch
) => {
	dispatch(
		loadPageArea<IPurchasesArea>(
			initialState.pageAreaName,
			"monthPage",
			pageID,
			(purchasesArea) => {
				console.log("purchasesArea", purchasesArea);

				purchasesArea.purchasesLists.forEach((pl) => {
					dispatch(todoActions.setList(pl.list, `${PURCHASES_LIST}_${pl.id}`));
				});
			}
		)
	);
};

const purchasesAreaActions = {
	...getPageAreaActions<IPurchasesArea>(),
	addList: (purchasesList: IPurchasesList) =>
		createAction(ADD_PURCHASES_LIST, purchasesList),
	deleteList: (purchasesListId: number) =>
		createAction(DELETE_PURCHASES_LIST, purchasesListId),
};

export const addPurchasesList = (purchasesList: ITodoList): ThunkType => async (
	dispatch
) => {
	let id = await todosService.createList(purchasesList);

	dispatch(
		purchasesAreaActions.addList({
			id: id,
			list: purchasesList,
		})
	);
};

export const deletePurchasesList = (
	purchasesList: ITodoList
): ThunkType => async (dispatch) => {
	dispatch(purchasesAreaActions.deleteList(purchasesList.id));
};

export type PurchasesAreaActions = ActionsUnion<typeof purchasesAreaActions>;
type ThunkType = BaseThunkType<PurchasesAreaActions>;
